using Crowdfund.API;
using Crowdfund.Data;
using Crowdfund.model;
using Crowdfund.Options;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace Crowdfund.Services
{
    public class TransactionServices : ITransactionService
    {
        private readonly CrowdfundDbContext dbContext = new CrowdfundDbContext();
        public TransactionOption CreateTransaction(int backerId, int rewardPackageOptionId)
        {
            if (backerId == 0) return null;
            Backer backer = dbContext.Backers.Find(backerId);
            if (backer == null) return null;
            RewardPackage rewardPackage = dbContext.RewardPackages.Where(o => o.Id == rewardPackageOptionId).Include(o => o.Project).SingleOrDefault();
            if (rewardPackage == null) return null;
            Transaction transaction = new Transaction { Backer = backer, RewardPackage = rewardPackage, Amount = rewardPackage.Price };
            dbContext.Transactions.Add(transaction);
            dbContext.SaveChanges();
            TransactionOption transactionOption = new TransactionOption
            {
                BackerName = backer.LastName + " " + backer.FirstName + " ",
                TransactionId = transaction.Id,
                BackerId = backer.Id,
                Amount = transaction.Amount,
                ProjectName = transaction.RewardPackage.Project.Title,
                RewardPackage = AddRewardPackageToTransaction(rewardPackageOptionId)
            };
            return UpdateCurrentBudget(transactionOption);
        }
        public TransactionOption UpdateCurrentBudget(TransactionOption transactionOption)
        {
            Project project = dbContext.Projects
                .Where(o => o.Id == transactionOption.RewardPackage.ProjectId)
                .Include(o => o.ProjectCreator)
                .SingleOrDefault();
            project.CurrentBudget += transactionOption.RewardPackage.Price;
            dbContext.SaveChanges();
            return transactionOption;
        }


        public TransactionOption GetTransaction(int transactionId)
        {
            Transaction transaction = dbContext.Transactions
                .Include(o => o.Backer)
                .FirstOrDefault(x => x.Id == transactionId);
            Backer backer = dbContext.Backers.Find(transaction.Backer.Id);

            List<int> rewardpackages = new List<int>();
            List<TransactionPackage> tranactionPackages = dbContext.TransactionPackages
                .Include(rp => rp.RewardPackage)
                .Where(rp => rp.Transaction.Equals(transaction))
                .ToList();

            tranactionPackages.ForEach(rp => rewardpackages.Add(rp.RewardPackage.Id));

            TransactionOption transactionOption = new TransactionOption
            {
                BackerName = backer.LastName + " " + backer.FirstName + " ",
                TransactionId = transaction.Id

                //ProjectId     = project.Id,
                //RewardPackages = rewardpackages
            };

            return transactionOption;
        }

        private RewardPackageOption AddRewardPackageToTransaction(int rewardPackageId)
        {
            RewardPackage rewardPackage = dbContext.RewardPackages.Where(o => o.Id == rewardPackageId).Include(o => o.Project).SingleOrDefault();
            RewardPackageOption rewardPackageOption = new RewardPackageOption
            {
                Id = rewardPackage.Id,
                Price = rewardPackage.Price,
                Reward = rewardPackage.Reward,
                ProjectId = rewardPackage.Project.Id,
                ProjectName = rewardPackage.Project.Title
            };
            return rewardPackageOption;
        }

        public TransactionOption FindTransactionById(int id)
        {
            Transaction transaction = dbContext.Transactions.Find(id);
            if (transaction == null) return null;
            List<int> Rewardpackages = new List<int>();
            return new TransactionOption
            {
                BackerName = transaction.Backer.LastName,
                TransactionId = transaction.Id
                //RewardPackages = transaction.TransactionPackages.
            };
        }
        private static RewardPackageOption GetRewardPackageOptionsFromRewardPackage(RewardPackage rewardPackage)
        {
            return new RewardPackageOption
            {
                Reward = rewardPackage.Reward,
                Price = rewardPackage.Price,
                Id = rewardPackage.Id,
                ProjectId = rewardPackage.Project.Id
            };
        }
        public List<TransactionOption> GetAllTransactions()
        {
            List<Transaction> tr = dbContext.Transactions
                .Include(o => o.RewardPackage)
                .ThenInclude(o => o.Project)
                .Include(o => o.Backer)
                .ToList();
            List<TransactionOption> trOpt = new List<TransactionOption>();
            tr.ForEach(tr => trOpt.Add(new TransactionOption
            {
                TransactionId = tr.Id,
                RewardPackageId = tr.RewardPackage.Id,
                BackerId = tr.Backer.Id,
                ProjectName = tr.RewardPackage.Project.Title,
                RewardPackage = GetRewardPackageOptionsFromRewardPackage(tr.RewardPackage)
                //RewardPackages = transaction.TransactionPackages.
            }));

            return trOpt;
        }
        public List<TransactionOption> GetMyTransactions(int backerId)
        {
            List<Transaction> tr = dbContext.Transactions
                .Include(o => o.RewardPackage)
                .ThenInclude(o => o.Project)
                .Include(o => o.Backer)
                .Where(o => o.Backer.Id == backerId)
                .ToList();
            List<TransactionOption> trOpt = new List<TransactionOption>();
            tr.ForEach(tr => trOpt.Add(new TransactionOption
            {
                TransactionId = tr.Id,
                RewardPackageId = tr.RewardPackage.Id,
                BackerId = tr.Backer.Id,
                Amount = tr.Amount,
                ProjectName = tr.RewardPackage.Project.Title,
                RewardPackageName = tr.RewardPackage.Reward,
                RewardPackage = GetRewardPackageOptionsFromRewardPackage(tr.RewardPackage)
                //RewardPackages = transaction.TransactionPackages.
            }));
            return trOpt;
        }

        public List<TransactionOption> BackedProjects(int backerId)
        {
            List<Transaction> tr = dbContext.Transactions
                .Include(o => o.RewardPackage)
                .ThenInclude(o => o.Project)
                .Include(o => o.Backer)
                .Where(o => o.Backer.Id == backerId)
                .ToList();
            List<TransactionOption> trOpt = new List<TransactionOption>();
            tr.ForEach(tr => trOpt.Add(new TransactionOption
            {
                TransactionId = tr.Id,
                RewardPackageId = tr.RewardPackage.Id,
                BackerId = tr.Backer.Id,
                Amount = tr.Amount,
                ProjectName = tr.RewardPackage.Project.Title,
                ProjectId = tr.RewardPackage.Project.Id,
                RewardPackageName = tr.RewardPackage.Reward,
                RewardPackage = GetRewardPackageOptionsFromRewardPackage(tr.RewardPackage)
                //RewardPackages = transaction.TransactionPackages.
            }));
            //var DistinctItems = trOpt.GroupBy(x => x.ProjectName).Select(y => y.First());
            List<TransactionOption> DistinctItems = trOpt.GroupBy(x => new { x.ProjectName, x.ProjectId }).Select(group => group.First()).ToList();
            List<TransactionOption> pOpt = new List<TransactionOption>();
            foreach (var item in DistinctItems)
            {
                pOpt.Add(new TransactionOption { ProjectName = item.ProjectName, ProjectId = item.ProjectId });
            }
            return pOpt;

        }
    }
}
