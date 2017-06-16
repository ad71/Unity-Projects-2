import matplotlib.pyplot as plt
import matplotlib.animation as animation
from matplotlib import style
import numpy as np

style.use('ggplot')

fig = plt.figure()
ax2 = fig.add_subplot(1, 1, 1)

def animate(i):
	graph_data = open('C:\\Users\\Aman Deep Singh\\Documents\\Unity-2\\Car AI GA\\Assets\\Data\\data.txt', 'r').read()
	lines = graph_data.split('\n')
	xs = []
	msas = []
	tss = []
	mmts = []
	mbts = []
	coms = []
	ms = []
	sls = []
	ssas = []
	wd4s = []
	wb4s = []
	wt4s = []
	s2nwds = []
	ss = []
	bc1s = []
	bc2s = []
	ams = []
	l2sas = []
	tspds = []
	for line in lines:
		if len(line) > 1:
			x, msa, ts, mmt, mbt, com, m, sl, ssa, wd4, wb4, wt4, s2nwd, s, bc1, bc2, am, l2sa, tspd, e = line.split(',')
			xs.append(x)
			msas.append(msa)
			tss.append(ts)
			mmts.append(mmt)
			mbts.append(mbt)
			coms.append(com)
			ms.append(m)
			sls.append(sl)
			ssas.append(ssa)
			wd4s.append(wd4)
			wb4s.append(wb4)
			wt4s.append(str(float(wt4) / 10))
			s2nwds.append(s2nwd)
			ss.append(s)
			bc1s.append(bc1)
			bc2s.append(bc2)
			ams.append(am)
			l2sas.append(l2sa)
			tspds.append(tspd)
	
	ax2.clear()
	ax2.plot(xs, msas, 'r-', label='Max Steer Angle')
	ax2.plot(xs, tss, 'b-', label='Top Speed')
	ax2.plot(xs, mmts, 'g-', label='Max Motor Torque')
	ax2.plot(xs, mbts, 'c-', label='Max Braking Torque')
	ax2.plot(xs, coms, 'm-', label='Center Of Mass-y')
	ax2.plot(xs, ms, 'y-', label='Mass')
	ax2.plot(xs, sls, 'k-', label='Sensor Length')
	ax2.plot(xs, ssas, 'y-.', label='Sensor Skew Angle')
	# ax2.plot(xs, wd4s, 'r--', label='Four wheel drive')
	# ax2.plot(xs, wb4s, 'b--', label='Four wheel brake')
	# ax2.plot(xs, wt4s, 'g--', label='Four wheel turn')
	ax2.plot(xs, s2nwds, 'c--', label='Next waypoint switch')
	# ax2.plot(xs, ss, 'm--', label='Sense enabled')
	ax2.plot(xs, bc1s, 'y--', label='Brake threshold speed')
	ax2.plot(xs, bc2s, 'k--', label='Brake threshold steer')
	ax2.plot(xs, ams, 'g-.', label='Avoid multiplier')
	# ax2.plot(xs, l2sas, 'b-.', label='Steer lerp')
	# ax2.plot(xs, tspds, 'c-.', label='Turning speed')

	ax2.legend()

ani = animation.FuncAnimation(fig, animate, interval=1000)
plt.show()