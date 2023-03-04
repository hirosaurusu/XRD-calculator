# XRD-calculator

1. How to use this app  
 [XRD_calculator.exe] needs [XRD_ParameterData] folder, which restores the lattice constants and crystal structure information of various materials. 
You cannot open this app if you miss this folder in the same directory. 
The app covers cubic, hexagonal, and tetragonal crystal structures.

1. XRD_ParameterData folder  
 The data format follows as below.  
 
(Cubic : Al)  
	l1 : 3  
	l2 : 4.04934  

(Hexagonal : AlN)  
	l1 : 5  
	l2 : 3.12859  
	l3 : 5.01695  


(Tetragonal : TiO2)  
	l1 : 6  
	l2 : 3.74  
	l3 : 9.39  

Line 1 is the crystal structure. Cubic --> 3, Hexagonal --> 5, Tetragonal --> 6.  
Line 2 and 3 are the lattice constants along a and c axis, respectively.
