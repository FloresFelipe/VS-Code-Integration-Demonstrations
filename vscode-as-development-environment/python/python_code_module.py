import os
import ctypes
 
class Debug:
    """
    A utility class for debugging purposes.
    """

    @staticmethod
    def show_process_id():
        """
        Display the process ID of the current running program.

        Returns:
        int: The process ID.
        """
        import os
        import ctypes
        ctypes.windll.user32.MessageBoxW(None, "Process name: niPythonHost.exe and Process ID: " + str(os.getpid()), "Attach debugger", 0)
        

class Math:
    """
    A simple class for performing basic mathematical operations.
    """

    def add(self, a, b):
        """
        Add two numbers.

        Parameters:
        a (float or int): The first number.
        b (float or int): The second number.

        Returns:
        float or int: The sum of a and b.
        """
        return a + b

    def multiply(self, a, b):
        """
        Multiply two numbers.

        Parameters:
        a (float or int): The first number.
        b (float or int): The second number.

        Returns:
        float or int: The product of a and b.
        """
        return a * b
