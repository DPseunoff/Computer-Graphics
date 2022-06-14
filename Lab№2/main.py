from matplotlib import pyplot as plt
from mpl_toolkits.mplot3d.art3d import Poly3DCollection
import numpy as np

fig = plt.figure()
ax = fig.add_subplot(111, projection='3d')

v = np.array([[1, -1.5, -1], [-1, -1.5, -1], [-1, 1.5, -1], [1, 1.5, -1], [0, -1, 1], [0, 1, 1]])
ax.scatter3D(v[:, 0], v[:, 1], v[:, 2])


xar = np.array([[0, 0, 0], [1, 0, 0]])

verts = [
          [v[0], v[1], v[2], v[3]],
          [v[0], v[4], v[5], v[3]],
          [v[1], v[4], v[5], v[2]],
          [v[0], v[4], v[1]],
          [v[3], v[5], v[2]]
        ]

ax.add_collection3d(Poly3DCollection(verts, facecolors=fig.get_facecolor(), linewidths=1, edgecolors='r'))

ax.set_xlabel('X', fontsize=13, color='blue')
ax.set_ylabel('Y', fontsize=13, color='blue')
ax.set_zlabel('Z', fontsize=13, color='blue')

plt.show()
