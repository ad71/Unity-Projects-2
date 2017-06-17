import matplotlib.pyplot as plt
import matplotlib.animation as animation
from matplotlib import style
import numpy as np
import os

style.use('ggplot')

userhome = os.path.expanduser('~')
filepath = userhome + "\\Desktop\\GA\\fitness.txt"
# For debugging, use filepath 'C:\\Users\\Aman Deep Singh\\Documents\\Unity-2\\Car AI GA\\Assets\\Data\\fitness.txt'

fig = plt.figure()
ax1 = fig.add_subplot(1, 1, 1) # similar to octave's subplotting system
# ax2 = plt.figure().add_subplot(1, 1, 1)

# An animation function is like a draw loop. It'll keep running and animating the plot
def animate(i):
	graph_data = open(filepath, 'r').read()
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

# def animate_genetic_data(i):
	# graph_data = open('C:\\Users\\Aman Deep Singh\\Documents\\Unity-2\\Car AI GA\\Assets\\Data\\data.txt', 'r').read()
	# lines = graph_data.split('\n')
	# xs = []
	# msas = []
	# tss = []
	# mmts = []
	# mbts = []
	# coms = []
	# ms = []
	# sls = []
	# ssas = []
	# wd4s = []
	# wb4s = []
	# wt4s = []
	# s2nwds = []
	# ss = []
	# bc1s = []
	# bc2s = []
	# ams = []
	# l2sas = []
	# tspds = []

	# for line in lines:
	# 	if len(line) > 1:
	# 		x, msa, ts, mmt, mbt, com, m, sl, ssa, wd4, wb4, wt4, s2nwd, s, bc1, bc2, am, l2sa, tspd, e = line.split(',')
	# 		xs.append(x)
	# 		msas.append(msa)
	# 		tss.append(ts)
	# 		mmts.append(mmt)
	# 		mbts.append(mbt)
	# 		coms.append(com)
	# 		ms.append(m)
	# 		sls.append(sl)
	# 		ssas.append(ssa)
	# 		wd4s.append(wd4)
	# 		wb4s.append(wb4)
	# 		wt4s.append(str(float(wt4) / 10))
	# 		s2nwds.append(s2nwd)
	# 		ss.append(s)
	# 		bc1s.append(bc1)
	# 		bc2s.append(bc2)
	# 		ams.append(am)
	# 		l2sas.append(l2sa)
	# 		tspds.append(tspd)
	
	# ax2.clear()
	# ax2.plot(xs, msas, 'r-', label='Max Steer Angle')
	# ax2.plot(xs, tss, 'b-', label='Top Speed')
	# ax2.plot(xs, mmts, 'g-', label='Max Motor Torque')
	# ax2.plot(xs, mbts, 'c-', label='Max Braking Torque')
	# ax2.plot(xs, coms, 'm-', label='Center Of Mass-y')
	# ax2.plot(xs, ms, 'y-', label='Mass')
	# ax2.plot(xs, sls, 'k-', label='Sensor Length')
	# ax2.plot(xs, ssas, 'y-.', label='Sensor Skew Angle')
	# ax2.plot(xs, wd4s, 'r--', label='Four wheel drive')
	# ax2.plot(xs, wb4s, 'b--', label='Four wheel brake')
	# ax2.plot(xs, wt4s, 'g--', label='Four wheel turn')
	# ax2.plot(xs, s2nwds, 'c--', label='Next waypoint switch')
	# ax2.plot(xs, ss, 'm--', label='Sense enabled')
	# ax2.plot(xs, bc1s, 'y--', label='Brake threshold speed')
	# ax2.plot(xs, bc2s, 'k--', label='Brake threshold steer')
	# ax2.plot(xs, ams, 'g-.', label='Avoid multiplier')
	# ax2.plot(xs, l2sas, 'b-.', label='Steer lerp')
	# ax2.plot(xs, tspds, 'c-.', label='Turning speed')

	# ax2.legend()
# ani stores the reference to the FuncAnimation method instance of the animation class of matplotlib.
# Arguments(where?, function_to_animate, interval_to_animate)
ani = animation.FuncAnimation(fig, animate, interval=1000)
# ani2 = animation.FuncAnimation(fig, animate_genetic_data, interval=2000)
plt.show();