import numpy as np
from matplotlib import cm
import matplotlib.pyplot as plt
from matplotlib.widgets import TextBox
from matplotlib.colors import LightSource



def PlotHalfSphere(radius, precision, transparency):
    phi = []
    theta = []

    pointTheta = 0
    pointPhi = 0

    dTheta = (0.5 * pi) / (precision - 1)
    dPhi = (2 * pi) / (precision - 1)

    tmp = []
    for i in range(precision):
        tmp.append(pointTheta)
    theta.append(tmp)

    for i in range(precision - 1):
        pointTheta = pointTheta + dTheta
        tmp = []
        for i in range(precision):
            tmp.append(pointTheta)
        theta.append(tmp)

    tmp = []
    tmp.append(pointPhi)
    for i in range(precision - 1):
        pointPhi = pointPhi + dPhi
        tmp.append(pointPhi)

    for i in range(precision):
        phi.append(tmp)

    x = radius * np.sin(theta) * np.cos(phi)
    y = radius * np.sin(theta) * np.sin(phi)
    z = radius * np.cos(theta)

    ls = LightSource(azdeg=360, altdeg=0)
    rgb = ls.shade(x, cmap=cm.Greys, vert_exag=0.1, blend_mode='soft')
    bsl = ls.blend_hsv(rgb, np.expand_dims(x * 0.8, 2))

    #axis.plot_surface(x, y, z, color=[1, 1, 1], alpha=transparency)
    #z = np.zeros((precision, precision))
    #axis.plot_surface(x, y, z, color=[1, 1, 1], alpha=transparency)

    axis.plot_surface(x, y, z, color=[1, 0.4, 0.3], alpha=transparency, facecolors=bsl, shade=False)

    plt.show()


def AxisInstallation():
    axis.set_xlim([-radius - 0.5, radius + 0.5])
    axis.set_ylim([-radius - 0.5, radius + 0.5])
    axis.set_zlim([0, 2 * radius - 0.2 * radius])
    axis.set_xlabel('X', fontsize=15, color='blue')
    axis.set_ylabel('Y', fontsize=15, color='blue')
    axis.set_zlabel('Z', fontsize=15, color='blue')


def ChangePrecision(input):
    axis.clear()
    AxisInstallation()
    try:
        PlotHalfSphere(radius, int(input), transparency)
    except Exception as e:
        print('Selected Precision {0} can\'t be used!'.format(input))


pi = np.pi
radius = 2
transparency = 1

f = plt.figure()
f.canvas.manager.set_window_title('Лабораторная работа №3')
f.canvas.mpl_disconnect(f.canvas.manager.key_press_handler_id)

axis = f.add_subplot(111, projection='3d')
AxisInstallation()

precisionField = plt.axes([0.5, 0.05, 0.1, 0.05])

precisionTextBox = TextBox(precisionField, 'Точность: ', '20')
precisionTextBox.on_submit(ChangePrecision)

PlotHalfSphere(radius, 20, transparency)
