﻿namespace Mutate4l.Cli
{
    public enum TokenType
    {
        _CommandsBegin,
        Interleave,
        Constrain,
        Slice,
        Explode,
        _CommandsEnd,
        _OptionsBegin,
        Start,
        Pitch,
        Ranges,
        Counts,
        Mode,
        _OptionsEnd,
        _TestOptionsBegin,
        GroupOneToggleOne,
        GroupOneToggleTwo,
        GroupTwoToggleOne,
        GroupTwoToggleTwo,
        DecimalValue,
        _TestOptionsEnd,
        Colon,
        Destination,
        _ValuesBegin,
        ClipReference,
        Number,
        MusicalDivision,
        _ValuesEnd,
        Unset
    }
}
