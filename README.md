# Annoyances.Net
Miscellaneous extensions to the .NET framework

This library adds some extra functionality to .NET -- some things that just seem to be missing, plus some general-purpose utilities.

## .NET class extension methods

These methods are all extension methods so they appear seamlessly on the framework classes and are discoverable from Visual Studio's auto-completion.

### Array extensions

`Select` enumerates through all the elements of a rectangular array

```csharp
var array = new[,,] { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } } };
var transformed = array.Select (a => a * 2).ToArray(); // { 2, 4, 6, 8, 10, 12, 14, 16 }
```

`ToJaggedArray` converts a rectangular array to the equivalent jagged array

```csharp
int[,] rectangularArray = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
int[][] jaggedArray = rectangularArray.ToJaggedArray();
```

`Row` and `Column` enumerate through the elements of one row or column from a rectangular array

```csharp
int[,] rectangularArray = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
int sum = rectangularArray.Column(1).Sum(); // = 2 + 5 + 8 = 15
```

`ToByteArray` converts a char array to a byte array using the specified encoding

```csharp
char[] chars = new[] { 'H', 'e', 'l', 'l', 'o', '\n', 'W', 'o', 'r', 'l', 'd' };
byte[] bytes = chars.ToByteArray(Encoding.ASCII);
MemoryStream ms = new MemoryStream(bytes);
StreamReader sw = new StreamReader(ms);
string line = sw.ReadLine(); // "Hello"
```

### Color extensions

Various methods for working with HSI, HSL and HSV colours.

### Enumerable extensions

Various `...OrDefault` versions of existing LINQ methods: `MaxOrDefault`, `MinOrDefault`, `AverageOrDefault`.

`TakeEvery` for working with every *n*th element of a sequence.

```csharp
int[] list = new[] { 1, 2, 3, 4, 5, 6 };
int[] firstOfEachPair = list.TakeEvery(2).ToArray(); // [1, 3, 5]
int[] secondOfEachPair = list.Skip(1).TakeEvery(2).ToArray(); // [2, 4, 6]
```

`Shuffle` for randomising the order of a sequence.

```csharp
int[] list = new[] { 1, 2, 3, 4, 5, 6 };
int[] random = list.Shuffle().ToArray(); // e.g. [ 6, 1, 2, 4, 5, 3 ]
```

`Cycle` for looping endlessly over a sequence.

```csharp
string[] redGreenRefactor = new[] { "red", "green", "refactor" };
string[] twoCycles = redGreenRefactor.Cycle().Take(6).ToArray();
// ["red", "green", "refactor", "red", "green", "refactor"]
```

`Permute` for getting all permutations of the elements in a sequence.

```csharp
int[] array = new[] {1, 2, 3};
IEnumerable<IEnumerable<int>> permuted = array.Permute();
// permuted contains [ [1, 2, 3], [1, 3, 2], [2, 1, 3], [2, 3, 1], [3, 1, 2], [3, 2, 1] ])
```

### Collection extensions

`RemoveFirst` to remove the first element matching a condition.

```csharp
var numbers = new List<int> { 5, 10, 15, 20 };
numbers.RemoveFirst(n => n > 10); // { 5, 10, 20 }
```

### List extensions

A version on `IndexOf` that takes a search condition. The standard version requires a reference to the element you're looking for.

```csharp
var passwords = new[] { "bad", "average!", "0_lksdfs_G00D/xx", "anotherlongonehere" };
int goodIndex = passwords.IndexOf(p => p.Length > 10); // 2
```

### String extensions

`StripTags` strips all the HTML tags from a string

```csharp
string html = "<div style=\"somestyle\">This is the <strong>content</strong> we want</div>";
string stripped = html.StripTags(); // "This is the content we want"
```

`IndexOfAny` and `LastIndexOfAny` find indexes locate the first/last occurrence of any of the supplied strings within another string

```csharp
string quote = "It was the best of times, it was the worst of times.";
var terms = new string[] { "best", "worst", "times" };
string match;

int first = quote.IndexOfAny(terms, StringComparison.InvariantCulture, out match);
// first = 11, match = "best"

int last = quote.LastIndexOfAny(terms, StringComparison.InvariantCulture, out match);
// last = 46, match = "times"
```

`ToByteArray` converts a string to a byte array using the specified encoding

```csharp
byte[] bytes = "somestring".ToByteArray(Encoding.UTF8);
```

`ToCamelCase` and `FromCamelCase` convert strings to and from *CamelCase* format.

```csharp
string sentence = "some sentence to convert";
string camel = sentence.ToCamelCase(CamelCaseStartsWith.LowerCase, CultureInfo.InvariantCulture);
// someSentenceToConvert
```

## `Exposed`

**Dangerous** but very cool. This class exposes the non-public bits of another class so you can access them easily without having to write complex code with reflection.

```csharp
dynamic e = new Exposed(someClass);
e.PrivateProperty = 123;
e.DoDangerousThing();
```

Don't use it! Unless you need it. Maybe not even then.

## `NotNull`

A helper class to specify that parameters should not be `null`. Gets the null checks out of the way at construction time.

```csharp
NotNull<object> thing = NotNull<object>.Create(null); // throws an ArgumentNullException
```

### Permutation

Utilities for working with permutations.

`GetIndexes` gets the sequence of 0-based index position groups.

```csharp
GetIndexes(2, 4);
// = { {0, 1}, {0, 2}, {0, 3}, {1, 2}, {1, 3}, {2, 3} , {2, 4}, {3, 4} }
// which represents the indexes of all distinct pairs from a set of four elements.
```

# Licence

MIT: http://pxc.mit-license.org/2014/
