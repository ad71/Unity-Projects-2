import matplotlib.pyplot as plt
import matplotlib.animation as animation
from matplotlib import style
import numpy as np

style.use('fivethirtyeight')

fig = plt.figure()
ax1 = fig.add_subplot(1, 1, 1) # similar to octave's subplotting system

# An animation function is like a draw loop. It'll keep running and animating the plot
def animate(i):
	graph_data = open('C:\\Users\\Aman Deep Singh\\Documents\\Unity-2\\Car AI GA\\Assets\\Data\\fitness.txt', 'r').read()
	lines = graph_data.split('\n')
	xs = []
	ys = []
	for line in lines:
		if len(line) > 1:
			x, y = line.split(',')
			xs.append(x)
			ys.append(y)

	# clearing doesn't cost much but is better
	ax1.clear()
	ax1.plot(xs, ys)

# ani stores the reference to the FuncAnimation method instance of the animation class of matplotlib.
# Arguments(where?, function_to_animate, interval_to_animate)
ani = animation.FuncAnimation(fig, animate, interval=1000)
plt.show();