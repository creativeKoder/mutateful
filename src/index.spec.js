const tape = require('tape');

/*
tape('Constrain Note Start', (test) => {
    var notesToMutate = [
        new Note(36, "0.25", "0.25", 100, 0),
        new Note(36, "1", "0.5", 100, 0),
        new Note(36, "4", "0.25", 100, 0),
        new Note(36, "3.5", "0.25", 100, 0)
    ];
    var notesToSourceFrom = [
        new Note(36, "0.5", "0.25", 100, 0),
        new Note(36, "3", "0.5", 100, 0),
        new Note(36, "4", "0.25", 100, 0),
        new Note(36, "5", "0.25", 100, 0)
    ];

    var clipActions = new ClipActions();
    var results = clipActions.process(Action.Constrain, notesToMutate, notesToSourceFrom, {
        constrainNoteStart: true,
        constrainNotePitch: false
    });

    test.equal(results[0].getStartAsString(), "0.5000", `Note start values should be equal. Actual: ${results[0].getStartAsString()} Expected: 0.5000`);
    test.equal(results[1].getStartAsString(), "0.5000", `Note start values should be equal. Actual: ${results[1].getStartAsString()} Expected: 0.5000`);
    test.equal(results[2].getStartAsString(), "4.0000", `Note start values should be equal. Actual: ${results[2].getStartAsString()} Expected: 4.0000`);
    test.equal(results[3].getStartAsString(), "3.0000", `Note start values should be equal. Actual: ${results[3].getStartAsString()} Expected: 3.0000`);

    test.end();
});

tape('Constrain Note Pitch', (test) => {
    var notesToMutate = [
        new Note(35, "0.25", "0.25", 100, 0),
        new Note(38, "1", "0.5", 100, 0),
        new Note(41, "4", "0.25", 100, 0),
        new Note(30, "3.5", "0.25", 100, 0)
    ];
    var notesToSourceFrom = [
        new Note(36, "0.5", "0.25", 100, 0),
        new Note(40, "3", "0.5", 100, 0),
        new Note(34, "4", "0.25", 100, 0),
        new Note(39, "5", "0.25", 100, 0)
    ];

    var clipActions = new ClipActions();
    var results = clipActions.process(Action.Constrain, notesToMutate, notesToSourceFrom, {
        constrainNoteStart: false,
        constrainNotePitch: true
    });

    test.equal(results[0].getPitch(), 36, `Pitch values should be equal. Actual: ${results[0].getPitch()} Expected: 36`);
    test.equal(results[1].getPitch(), 39, `Pitch values should be equal. Actual: ${results[1].getPitch()} Expected: 39`);
    test.equal(results[2].getPitch(), 40, `Pitch values should be equal. Actual: ${results[2].getPitch()} Expected: 40`);
    test.equal(results[3].getPitch(), 34, `Pitch values should be equal. Actual: ${results[3].getPitch()} Expected: 34`);

    test.end();
});
*/

tape('Interleave notes', (test) => {
    var src = {
        getNotes: () => {
            return [
                new Note(36, "0.5", "1", 100, 0),
                new Note(37, "1.5", "0.5", 100, 0),
                new Note(38, "2.0", "1.0", 100, 0)
            ];
        },
        getLength: () => {
            return new Big(4);
        }
    };
    var dst = {
        getNotes: () => {
            return [
                new Note(36, "0.5", "0.25", 100, 0),
                new Note(37, "1.75", "0.5", 100, 0),
                new Note(38, "2.5", "1.0", 100, 0)
            ];
        },
        getLength: () => {
            return new Big(4);
        }
    };
    var clipActions = new ClipActions();
    var resultClip = clipActions.process(Action.Interleave, dst, src, {
        interleaveMode: InterleaveMode.TimeRange,
        interleaveCountA: 1,
        interleaveCountB: 1,
        interleaveEventRangeA: new Big(1),
        interleaveEventRangeB: new Big(1)
    });
    var results = resultClip.notes;

    for (var i = 0; i < results.length; i++) {
        console.log("start", results[i].getStartAsString(), "dur", results[i].getDurationAsString(), results[i].getPitch());
    }
/*
    test.equal(results[1].getStartAsString(), "0");
    test.equal(results[2].getStartAsString(), "0");
    test.equal(results[3].getStartAsString(), "0");
    test.equal(results[4].getStartAsString(), "0", "");
    test.equal(results[5].getStartAsString(), "0", "");
    test.equal(results[6].getStartAsString(), "0", "");
*/

    test.end();
});