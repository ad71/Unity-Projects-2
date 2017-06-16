import matplotlib.pyplot as plt
import matplotlib.animation as animation
from matplotlib import style
import numpy as np

style.use('ggplot')

# A function that plots only probabilistic genetic data
fig = plt.figure()
ax1 = fig.add_subplot(111)

def animate(i):
	graph_data = open('C:\\Users\\Aman Deep Singh\\Documents\\Unity-2\\Car AI GA\\Assets\\Data\\data.txt', 'r').read()
	lines = graph_data.split('\n')
	xs = []
	wd4s = []
	wb4s = []
	wt4s = []
	ss = []
	l2sas = []
	tss = []
	for line in lines:
		if len(line) > 1:
			l = line.split(',')
			x = l[0]
			wd4 = l[8]
			wb4 = l[9]
			wt4 = l[10]
			s = l[12]
			l2sa = l[16]
			ts = l[17]
			xs.append(x)
			wd4s.append(wd4)
			wb4s.append(wb4)
			wt4s.append(str(float(wt4) / 10))
			ss.append(s)
			l2sas.append(l2sa)
			tss.append(ts)

	ax1.clear()
	ax1.plot(xs, wd4s, 'r', label='Four wheel drive')
	ax1.plot(xs, wb4s, 'b', label='Four wheel brake')
	ax1.plot(xs, wt4s, 'c', label='Four wheel turn')
	ax1.plot(xs, ss, 'g', label='Sense enabled')
	ax1.plot(xs, l2sas, 'y', label='Steer lerp')
	ax1.plot(xs, tss, 'm', label='Turning speed')
	ax1.legend()

ani = animation.FuncAnimation(fig, animate, interval=1000)
plt.show();