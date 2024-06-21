import numpy as np
a = [4, 3, 5, 7, 6, 8]
indices = [0, 1, 4]

print(np.take(a, indices))

# result >> array([4, 3, 6])
