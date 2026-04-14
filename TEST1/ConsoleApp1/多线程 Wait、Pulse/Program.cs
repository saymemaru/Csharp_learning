using 多线程_Wait_Pulse;

Producer producer = new();
Consumer consumer = new();

Thread producerThread = new(producer.Produce) { Name = "Producer thread" };
Thread consumerThread = new(consumer.Consume) { Name = "Consumer thread" };

producerThread.Start();
consumerThread.Start();

producerThread.Join();
consumerThread.Join();