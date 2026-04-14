using 多线程_Signaling;

Shared shared = new ();
Producer producer = new();
Consumer consumer = new();
ThreadStart threadStart1 = new(producer.Produce);
ThreadStart threadStart2 = new(consumer.Consume);
Thread producerThread  = new(threadStart1) { Name = "Producer thread"};
Thread consumerThread = new(threadStart2) { Name = "Consumer thread" };
producerThread.Start();
consumerThread.Start();



producerThread.Join();
consumerThread.Join();