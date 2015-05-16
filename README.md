# Annoyances.Net
Miscellaneous extensions to the .NET framework

This library adds some extra functionality to .NET -- some things that just seem to be missing, plus some general-purpose utilities.

## .NET class extension methods

These methods are all extension methods so they appear seamlessly on the framework classes and are discoverable from Visual Studio's auto-completion.

### Array extensions

`Select` enumerates through all the elements of a rectangular array

`ToJaggedArray` converts a rectangular array to the equivalent jagged array

`Row` and `Column` enumerate through the elements of one row or column from a rectangular array

`ToByteArray` converts a char array to a byte array using the specified encoding

### Color extensions

Various methods for working with HSI, HSL and HSV colours.

### Enumerable extensions

Various `...OrDefault` versions of existing LINQ methods: `MaxOrDefault`, `MinOrDefault`, `AverageOrDefault`.

`TakeEvery` for working with every *n*th element of a sequence.

`Shuffle` for randomising the order of a sequence.

`Cycle` for looping endlessly over a sequence.

`Permute` for getting all permutations of the elements in a sequence (e.g. [1, 2, 3] yields [ [1, 2, 3], [1, 3, 2], [2, 1, 3], [2, 3, 1], [3, 1, 2], [3, 2, 1] ]).

### Collection extensions

`RemoveFirst` to remove the first element matching a condition.

### String extensions

`StripTags` strips all the HTML tags from a string

`IndexOfAny` and `LastIndexOfAny` find indexes locate the first/last occurrence of any of the supplied strings within another string

`ToByteArray` converts a string to a byte array using the specified encoding

## `Exposed`

Slightly dangerous... very cool. This class exposes the non-public bits of another class so you can access them easily without having to write complex code with reflection.

    dynamic e = new Exposed(someClass);
    e.PrivateProperty = 123;
    e.DoDangerousThing();

## `NotNull`

A helper class to specify that parameters should not be `null`.

### Permutation

Utilities for working with permutations.

`GetIndexes` gets the sequence of 0-based index position groups:

    GetIndexes(2, 4) = { {0, 1}, {0, 2}, {0, 3}, {1, 2}, {1, 3}, {2, 3} , {2, 4}, {3, 4} }

which represents the indexes of all distinct pairs from a set of 4 elements.

# Licence

MIT: http://pxc.mit-license.org/2014/
