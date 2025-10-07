using 设计模式_状态模式;

Context context = new(new WonderState());

context.IsBusy = true;
context.Hour = 10;
context.Request();

context.Hour = 20;
context.Request();

context.Hour = 5;
context.Request();

context.IsBusy = false;
context.Hour = 10;
context.Request();

