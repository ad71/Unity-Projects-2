import matplotlib.pyplot as plt
import matplotlib.animation as animation
from matplotlib import style
import os
import platform

style.use('ggplot')

userhome = os.path.expanduser('~')
filepath = userhome + '\\Desktop\\GA\\data.txt'
# for teesting use filepath as C:\\Users\\Aman Deep Singh\\Documents\\Unity-2\\Assets\\Data\\data.txt

fig = plt.figure()
ax1 = fig.add_subplot(231)
ax2 = fig.add_subplot(232)
ax3 = fig.add_subplot(233)
ax4 = fig.add_subplot(234)
ax5 = fig.add_subplot(235)
ax6 = fig.add_subplot(236)

def animate(i):
	graph_data = open(filepath, 'r').read()
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

	ax1.clear()
	ax2.clear()
	ax3.clear()
	ax4.clear()
	ax5.clear()
	ax6.clear()
	# Ordered gene-number wise
	ax6.plot(xs, msas, 'g-', label='Max Steer Angle') 
	ax1.plot(xs, tss, 'b-', label='Top Speed')
	ax1.plot(xs, mmts, 'g-', label='Max Motor Torque')
	ax1.plot(xs, mbts, 'c-', label='Max Braking Torque')
	ax3.plot(xs, coms, 'b-', label='Center Of Mass-y')
	ax3.plot(xs, ms, 'c-', label='Mass')
	ax4.plot(xs, sls, 'g-', label='Sensor Length')
	ax4.plot(xs, ssas, 'k-', label='Sensor Skew Angle')
	ax5.plot(xs, wd4s, 'r-', label='Four wheel drive')
	ax5.plot(xs, wb4s, 'k-', label='Four wheel brake')
	ax5.plot(xs, wt4s, 'c-', label='Four wheel turn')
	ax4.plot(xs, s2nwds, 'c-', label='Next waypoint switch')
	ax3.plot(xs, ss, 'k-', label='Sense enabled')
	ax2.plot(xs, bc1s, 'r-', label='Brake threshold speed')
	ax2.plot(xs, bc2s, 'm-', label='Brake threshold steer')
	ax2.plot(xs, ams, 'y-', label='Avoid multiplier')
	ax6.plot(xs, l2sas, 'b-', label='Steer lerp')
	ax6.plot(xs, tspds, 'c-', label='Turning speed')
	ax1.legend()
	ax2.legend()
	ax3.legend()
	ax4.legend()
	ax5.legend()
	ax6.legend()

ani = animation.FuncAnimation(fig, animate, interval=1000)
plt.show();