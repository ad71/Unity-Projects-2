# Car AI 3
## CNNs learn to drive

This repository contains the environment for this project. The environment is just a simple track (circuit). The game was played by actual humans and the data was collected from the screencasts. An ensemble of convolutional neural networks were trained on this image dataset with the corresponding keypress as the label. About 85% accuracy was achieved, but we couldn't try complicated models due to hardware limitations. The convolutional neural network models, dataset and preprocessing functions are in [this repository](https://github.com/ad71/Practical-ML/tree/master/Python%20Plays).

![title](https://github.com/ad71/Genetic-Algorithms/blob/master/Car%20AI/ss_3.jpg)
![title](https://github.com/ad71/Genetic-Algorithms/blob/master/Car%20AI/ss_4.jpg)
![title](https://github.com/ad71/Genetic-Algorithms/blob/master/Car%20AI/ss_5.jpg)

# Car AI GA
## The Island project

This project is aimed at finding the mechanical properties of the fastest car that could meander an arbitrary track. The environment, created on Unity-C#, consists of a track on an island. The track is marked with waypoints. Cars can run on the tracks following the waypoints. Each car tries to steer towards the next closest waypoint. A function similar to the `seek` behavior by Craig Reynolds is used to handle the steering of the cars. A data-visualization tool was also built to help analyze the data generated. 

### The DNA sequence
Each DNA sequence contains 18 optimizable variables. Each element handles a physical property of the car. The 18 variables are:
1. maxSteerAngle
2. topSpeed
3. maxMotorTorque
4. maxBrakingTorque
5. centerOfMass.y
6. Mass
7. sensorLength
8. sensorSkewAngle
9. 4WheelDrive?
10. 4WheelBrake?
11. 4WheelTurn?
12. switchToNextWaypointDistance
13. sense()?
14. brakingConditions1
15. brakingConditions2
16. avoidMultiplier parameter
17. lerpToSteerAngle?
18. turningSpeed
The use of some of these variables will be apparent only after looking at the source code.

### Algorithm
A modified genetic algorithm is used to optimize  over the DNA sequence in order to find the set of parameters that allow a car to achieve the lowest track time. The lowest time recorded yet was about 16 seconds.

![title](https://github.com/ad71/Genetic-Algorithms/blob/master/Car%20AI/ss.jpg)
![title](https://github.com/ad71/Genetic-Algorithms/blob/master/Car%20AI/ss_1.jpg)
![title](https://github.com/ad71/Genetic-Algorithms/blob/master/Car%20AI/ss_2.jpg)

### Visualization
A [visualization tool](https://github.com/ad71/Data-Science/blob/master/Data%20Visualization/car_ai_genes.py) plots the genes across generations to help analyze the correlation between genes and [another](https://github.com/ad71/Data-Science/blob/master/Data%20Visualization/car_ai_genes_plot.py) plots the fitness of the respective genomes.

![title](https://github.com/ad71/Unity-Projects-2/blob/master/Car%20AI%20GA/ss.jpg)

### Analysis
The collected data ([here](https://github.com/ad71/Practical-ML/blob/master/GAmine/data_240.txt)) was used to analyze the trends and the effect of the algorithm on the average track-time. The data was fed into neural networks to predict the track-time given the 18 parameters. [Directory](https://github.com/ad71/Practical-ML/tree/master/GAmine)

![title](https://github.com/ad71/Unity-Projects-2/blob/master/Car%20AI%20GA/ss_3.jpg)