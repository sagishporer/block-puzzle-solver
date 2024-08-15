# Block Puzzle Solver
Simple brute force, backtracking, block puzzle solver. I needed this to put a wooden block puzzle back into his box. 

Note: Symmetric solutions are not removed.

### Run time 
- Less than a second to yield first solution for my 8x8, 13 pieces pentominoes puzzle and all tested puzzles.
- Less than 2 minutes (without output) to enumerate all 129,168 solutions for 8x8 pentomino puzzle (12 pentominoes + 2x2).
- Less than 1 minute (without output) to enumerate all 9,356 solutions for 6x10 pentomino puzzle (12 pentominoes).

### Input file format
- Line 1: [Rows] [Columns]
- Line 2+: shapes, seperated by empty line.

## Example
### Input
```
3 3
**
*
**
 
 *
***
```

### Output
```
Run time: 00:00:00.0012541
-----------------------------
112
122
112
-----------------------------
Run time: 00:00:00.0072183
-----------------------------
111
121
222
-----------------------------
Run time: 00:00:00.0073856
-----------------------------
211
221
211
-----------------------------
Run time: 00:00:00.0075029
-----------------------------
222
121
111
-----------------------------
Total run time: 00:00:00.0233688
```
