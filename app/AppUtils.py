class AppUtils:

    def generateHeader(subHeader: str) -> str:
        base = r"""
    ____             __                _____ __         ______________  ____ 
   / __ \____  _____/ /_____  _____   / ___// /_  ___  / / /_  __/ __ \/ __ \
  / / / / __ \/ ___/ //_/ _ \/ ___/   \__ \/ __ \/ _ \/ / / / / / / / / /_/ /
 / /_/ / /_/ / /__/ ,< /  __/ /      ___/ / / / /  __/ / / / / / /_/ / ____/ 
/_____/\____/\___/_/|_|\___/_/      /____/_/ /_/\___/_/_/ /_/  \____/_/      
        """
        if subHeader:
            base += "\n" + subHeader
        
        return base