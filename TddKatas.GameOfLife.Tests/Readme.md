# Conway´s Game of Life

El universo de Game of Life es una cuadrícula ortogonal, bidimensional e infinita de células cuadradas, cada una de las cuales se encuentra en uno de los dos estados posibles: viva o muerta. Cada célula interactúa con sus ocho vecinas, que son las célula que están horizontal, vertical o diagonalmente adyacentes a ella. En cada paso del tiempo se producen las siguientes transiciones:

- Cualquier célula viva con menos de dos vecinas vivas muere, como si la causa fuera la infrapoblación.
- Cualquier célula viva con dos o tres vecinas vivas pasa a la siguiente generación.
- Cualquier célula viva con más de tres vecinas vivas muere, como por sobrepoblación.
- Cualquier célula muerta con exactamente tres vecinas vivas se convierte en una célula viva, como por reproducción.

El patrón inicial constituye la semilla del sistema. 

La primera generación se crea aplicando las reglas mencionadas simultáneamente a cada célula de la semilla: los nacimientos y las muertes se producen simultáneamente, y el momento determinado en que esto ocurre se denomina tick (en pocas palabras, cada generación es una función pura de la anterior).
