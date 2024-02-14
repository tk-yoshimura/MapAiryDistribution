# MapAiryDistribution
 
In probability theory, the Map-Airy distribution (or Airy distribution of the 'Map'-type) describes a brownian excursion over a unit interval.  

This distribution is a special case of a stable distribution with shape parameter &alpha; = 3/2 and skewness parameter &beta; = 1.  
There is also a definition with &beta; = -1, which is inverted in the x-axis direction.  

## Definition
The Map-Airy distribution is as follows:  
![mapairy1](figures/mapairy1.svg)  

The scaling factor *c* of the stable distribution is standardize the following:  
![mapairy2](figures/mapairy2.svg)  

## Numerical Evaluation
The series expansion of Ai and Ai' with argument *x* squared is as follows:  
![mapairy3](figures/mapairy3.svg)  
![mapairy4](figures/mapairy4.svg)  

When |*x*| is large, the following equation can be used as an asymptotic expression:  
![mapairy5](figures/mapairy5.svg)  
![mapairy6](figures/mapairy6.svg)  

That is, when *x* &rarr; &infin;  
![mapairy10](figures/mapairy10.svg)  
That is, when *x* &rarr; -&infin;  
![mapairy11](figures/mapairy11.svg)  

Fortunately, when x is large, the exponential function annihilates and integral evaluation becomes easy.  
Otherwise, the coefficients must be evaluated by adding up with the series expansion of the exponential function.  
![mapairy7](figures/mapairy7.svg)  

The coefficients of the series expansion are obtained by the recurrence relation as follows:  
![mapairy8](figures/mapairy8.svg)  

![mapairy9](figures/mapairy9.svg)  

## Statistics

|stat|x|note|
|----|----|----|
|mean|0||
|mode|||
|variance|N/A|undefined|
|0.75-quantile|||
|0.9-quantile|||
|0.95-quantile||
|0.99-quantile|||

## Numeric Table


## Reference

## See Also
[LandauDistribution](https://github.com/tk-yoshimura/LandauDistribution)
