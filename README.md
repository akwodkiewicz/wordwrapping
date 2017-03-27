# wordwrapping
Dynamic programming algorithm for the word wrapping optimization problem.

The algorithm minimizes raggedness by minimizing the sum of the cubes of the spaces at the end of the lines (excluding last one).

Example in comparison to a greedy algorithm:

```
Greedy:

------    Line width: 6
aaa bb    Remaining space: 0
cc        Remaining space: 4
ddddd     Remaining space: 1

cost = 0^3 + 4^3 = 64
```
```
Dynamic:

------    Line width: 6
aaa       Remaining space: 3
bb cc     Remaining space: 1
ddddd     Remaining space: 1

cost = 3^3 + 1^3 = 28
```
