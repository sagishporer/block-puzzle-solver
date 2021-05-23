# Block Puzzle Solver
Simple brute force, backtracking, block puzzle solver. I needed this to put a wooden block puzzle back into his box.

Run time: 
- ~30 seconds to yield first solution for my 8x8, 13 pieces puzzle.
- 0.5 seconds to get all possible solutions for the 5x6 puzzle here: https://medium.com/@andreaiacono/backtracking-explained-7450d6ef9e1a

Input file format -
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
