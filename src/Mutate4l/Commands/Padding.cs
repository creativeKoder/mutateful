﻿using Mutate4l.Cli;
using Mutate4l.Core;
using Mutate4l.Utility;

namespace Mutate4l.Commands
{
    // Extracts notes of the current clips and inserts nothing in front, effectively padding them. 
    // An optional second argument enforces an abritrary total duration;
    public class PaddingOptions
    {
        [OptionInfo(type: OptionType.Default, 4 / 128f)]
        public decimal PadAmount { get; set; } = 2;

        public decimal Length { get; set; } = -1;
        
        public bool Post { get; set; } // If specified, adds padding to the end of the clip instead
    }

    // # desc: Adds silence (i.e. padding) at the start of a clip, or at the end of a clip if -post is specified. If -length is specified, padding is calculated so that the total length of the clip matches this. If length is shorter than the current clip length, the clip is cropped instead.
    public static class Padding
    {
        public static ProcessResultArray<Clip> Apply(Command command, params Clip[] clips)
        {
            var (success, msg) = OptionParser.TryParseOptions(command, out PaddingOptions options);
            if (!success)
            {
                return new ProcessResultArray<Clip>(msg);
            }
            return Apply(options, clips);
        }

        public static ProcessResultArray<Clip> Apply(PaddingOptions options, params Clip[] clips)
        {
            var processedClips = new Clip[clips.Length];

            for (var i = 0; i < clips.Length; i++)
            {
                var clip = new Clip(clips[i]);
                var padAmount = options.PadAmount;
                if (options.Length > 0)
                {
                    if (options.Length > clip.Length)
                    {
                        padAmount = options.Length - clip.Length;
                    }
                    else if (options.Length < clip.Length)
                    {
                        processedClips[i] = Crop.CropClip(clip, 0, options.Length);
                        continue;
                    }
                }

                clip.Length += padAmount;
                if (!options.Post)
                {
                    foreach (var noteEvent in clip.Notes)
                    {
                        noteEvent.Start += padAmount;
                    }
                }

                processedClips[i] = clip;
            }

            return new ProcessResultArray<Clip>(processedClips);
        }
    }
}