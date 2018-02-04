﻿using Mutate4l.Dto;
using Mutate4l.Options;
using Mutate4l.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mutate4l.Commands.Plumbing
{
    public class Monophonize
    {
        public static ProcessResult Apply(params Clip[] clips)
        {
            var processedClips = new List<Clip>();
            foreach (var clip in clips)
            {
                processedClips.Add(ClipUtilities.Monophonize(clip));
            }
            return new ProcessResult(processedClips.ToArray());
        }
    }
}