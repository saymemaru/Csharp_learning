using _2048;

Core core = new(4);
core.GeneragteNumber();
Core.PrintDoubleArray(core.Map);

while(true)
{
    GetKeyDown(core);
    if(core.IsMapChanged)
    {
        core.GeneragteNumber();
        core.IsMapChanged = false;
        Core.PrintDoubleArray(core.Map);
    }
}


void GetKeyDown(Core core)
{
    switch(Console.ReadLine())
    {
        case "w":
            core.Move(MoveDirection.Up);
            break;
        case "s":
            core.Move(MoveDirection.Down);
            break;
        case "a":
            core.Move(MoveDirection.Left);
            break;
        case "d":
            core.Move(MoveDirection.Right);
            break;

    }
    
}