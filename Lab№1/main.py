import numpy as np
import pylab as pb
from matplotlib.widgets import TextBox, Button

def plot_graphic(ax, a):
    phi = 0

    x = []
    y = []

    phi = phi + d
    xt = (a * np.sqrt(2) * np.cos(phi)) / (1 + (np.sin(phi) ** 2))
    yt = (a * np.sqrt(2) * np.sin(phi) * np.cos(phi)) / (1 + (np.sin(phi) ** 2))

    x.append(xt)
    y.append(yt)

    while phi < 2 * np.pi:
        phi = phi + d
        xt = (a * np.sqrt(2) * np.cos(phi))/(1 + (np.sin(phi)**2))
        yt = (a * np.sqrt(2) * np.sin(phi)*np.cos(phi)) / (1 + (np.sin(phi) ** 2))
        x.append(xt)
        y.append(yt)
        ax.plot(x, y, color='black')
        x.pop(0)
        y.pop(0)


    pb.draw()

'''
        else:
            while (a ** 2 * np.cos(2 * phi)) < 0:
                phi = phi + d
            p = np.sqrt(a ** 2 * np.cos(2 * phi))
            xt = p * np.cos(phi)
            yt = p * np.sin(phi)
            x.append(xt)
            y.append(yt)
            ax.plot(x, y, color='white')
            phi = phi + d

'''



def submit1(expression):
   global a
   a = eval(expression)


def submit2(expression):
   global d
   d = eval(expression)


def do(event):
    ax.cla()
    plot_graphic(ax, a)
    ax.relim()
    ax.autoscale_view()
    ax.grid()
    ax.set_xlabel('X', fontsize=13, color='blue')
    ax.set_ylabel('Y', fontsize=13, color='blue')
    ax.axhline(0, color='blue')
    ax.axvline(0, color='blue')


f, ax = pb.subplots()
f.subplots_adjust(bottom=0.2)
axButton = f.add_axes([0.13, 0.05, 0.1, 0.05])
btn = Button(ax=axButton, label='', color='green')
btn.on_clicked(do)
box1 = f.add_axes([0.3, 0.08, 0.1, 0.05])
text_box1 = TextBox(box1, "a:")
text_box1.on_submit(submit1)
box2 = f.add_axes([0.3, 0.02, 0.1, 0.05])
text_box2 = TextBox(box2, "d:")
text_box2.on_submit(submit2)
f.canvas.mpl_disconnect(f.canvas.manager.key_press_handler_id)

ax.grid()
pb.show()

