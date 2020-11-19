﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Options
{
    public class TransactionOption
    {
        public string BackerName { get; set; }
        public int TransactionId { get; set; }
        public List<int> RewardPackages { get; set; }
    }
}
