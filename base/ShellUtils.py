import os
import re

class ShellUtils:

    linePosition: int = 0

    header = r"""
    ____             __                _____ __         ______________  ____ 
   / __ \____  _____/ /_____  _____   / ___// /_  ___  / / /_  __/ __ \/ __ \
  / / / / __ \/ ___/ //_/ _ \/ ___/   \__ \/ __ \/ _ \/ / / / / / / / / /_/ /
 / /_/ / /_/ / /__/ ,< /  __/ /      ___/ / / / /  __/ / / / / / /_/ / ____/ 
/_____/\____/\___/_/|_|\___/_/      /____/_/ /_/\___/_/_/ /_/  \____/_/      
        """

    def clearScreen():        
        for _ in range(ShellUtils.linePosition):
            # reset cursor one line
            print("\033[F", end="")
            # clear line
            print("\033[K", end="")
        
        ShellUtils.linePosition = 0

    def write(text: str = ""):            
        lineCount = re.split(r'\r\n|\r|\n', text).__len__()
        ShellUtils.linePosition = ShellUtils.linePosition + lineCount
        print(text)
        