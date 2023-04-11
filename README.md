# KafkaNetCore # ApacheKafka #Kafka
Samples with kafka(apache) and .net core

What is Kafka?
    Apache Kafka is a distributed publish-suscribe messaging system that is designed to be fast, scalable and durable.
    Kafka stores streams of records(messages) in topics. Each record consist of a key/value and a timestamp.

Producer write data to topics and Consumer read data from topics.

Topic and logs
    Topic is a category to which records are published. It can have zero, one or many consumers that suscribe to the data written to it.
    For each topic, Kafka cluster maintains a partitioned log. Since Kafka is a distributed system, topics are partitioned and replicated across multiple nodes.

<!-- ![KafkaAnatomy](Images/Anatomy.png.jpg) -->
<div class="row">
    <img src="https://github.com/XiaoJiisus/KafkaNetCore/blob/main/Images/Anatomy.png" width="100" height="100">
</div>