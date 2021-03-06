﻿using Crowdfund.API;
using Crowdfund.Options;
using Crowdfund.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CrowdfundWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {

        private readonly ILogger<MediaController> _logger;
        private readonly IMediaService mediaService;
        public MediaController(ILogger<MediaController> logger, IMediaService _mediaService)
        {
            _logger = logger;
            mediaService = _mediaService;
        }
        [HttpGet("{projectId}")]
        public List<MediaOption> GetMediaForProject(int projectId)
        {
            return mediaService.FindAllMediaofProject(projectId);
        }
        [HttpPost]
        public MediaOption CreateMediaAndAddItToProject([FromForm]MediaOption mediaOption)
        {
            return mediaService.CreateMedia(mediaOption);
        }
        [HttpDelete("{mediaId}")]
        public bool DeleteMedia(int mediaId)
        {
            return mediaService.DeleteMedia(mediaId);
        }
        [HttpGet("{mediaId}")]
        public MediaOption FindMedia(int mediaId)
        {
            return mediaService.FindMedia(mediaId);
        }
        [HttpPut("{mediaId}")]
        public MediaOption UpdateMedia(int mediaId, MediaOption mediaOption)
        {
            return mediaService.UpdateMedia(mediaId, mediaOption);
        }
    }
}
