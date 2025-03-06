# KafkaConsole

## Kafka Event Processor

This is a simple .NET application that consumes events from a Kafka topic, enriches those events, and then publishes the enriched events back to another Kafka topic. It follows SOLID principles to make the code modular, extensible, and easy to maintain.

## Features

- **Kafka Consumer**: Consumes messages from a Kafka topic.
- **Event Enricher**: Processes and enriches events before they are sent back to Kafka.
- **Kafka Producer**: Publishes enriched events to a Kafka topic.
- **SOLID Principles**: The application follows SOLID principles to ensure clean, maintainable, and testable code.

## Requirements

- .NET 6+ SDK (or later)
- Apache Kafka running locally or on a remote server

### Kafka Topics:

- `input-topic` (for consuming messages)
- `output-topic` (for publishing enriched messages)

- Confluent Kafka .NET Client (Confluent.Kafka NuGet package)

## Prerequisites

Before running the application, ensure you have the following:

- **Kafka Cluster**: Set up and running. If you're testing locally, you can use Docker to run Kafka.
- **.NET SDK**: Install from Microsoft's website.
- **Confluent.Kafka NuGet Package**: This is used for Kafka consumer/producer operations.

You can install the Confluent.Kafka NuGet package by running the following command:
``` 
dotnet add package Confluent.Kafka
```

## Project Structure

The application is structured to follow the SOLID principles:

- **Interfaces**:
  - `IEventConsumer`: Interface for consuming Kafka events.
  - `IEventProducer`: Interface for publishing Kafka events.
  - `IEventEnricher`: Interface for enriching events.
- **Implementations**:
  - `KafkaEventConsumer`: Implementation of the Kafka event consumer.
  - `KafkaEventProducer`: Implementation of the Kafka event producer.
  - `EventEnricher`: Implementation of the event enricher.
  - `EventProcessor`: Central component that coordinates consuming, enriching, and producing events.

## Getting Started

1. **Clone the Repository**

   Clone the project to your local machine using:
   ```
   git clone https://github.com/SJLazar7/KafkaConsole.git
   cd KafkaConsole
2. **Update Kafka Configuration**

   Open the `Program.cs` file and update the following variables with your Kafka cluster details and topic names:
   ``` 
	csharp string bootstrapServers = "localhost:9092";  // Update with your Kafka server
	string inputTopic = "input-topic";          // Kafka topic to consume from
	string outputTopic = "output-topic";        // Kafka topic to produce to
3. **Run Kafka Locally (Optional)**

   If you don’t have Kafka running locally, you can start a Kafka server using Docker:
   ```
    docker-compose up -d
Make sure your `docker-compose.yml` file includes a setup for both Kafka and Zookeeper.

4. **Build and Run the Application**

   To build and run the application, execute the following commands:

The application will:
   - Consume events from the `input-topic`.
   - Enrich those events (for now, by appending " - Enriched").
   - Publish the enriched events to the `output-topic`.

5. **Verify in Kafka**

   You can use Kafka's command-line tools or any Kafka client (like Kafka Tool) to check the messages in the `input-topic` and `output-topic`.

## Code Overview

1. **Kafka Event Consumer (`KafkaEventConsumer`)**

   This class connects to a Kafka broker and consumes messages from a specified Kafka topic.

2. **Kafka Event Producer (`KafkaEventProducer`)**

   This class connects to a Kafka broker and publishes enriched events to a specified Kafka topic.

3. **Event Enricher (`SimpleEventEnricher`)**

   This class enriches the events. In this example, it simply appends " - Enriched" to the event message.

4. **Event Processor (`EventProcessor`)**

   This class coordinates the entire process. It consumes events, enriches them, and then produces the enriched events back to Kafka.

5. **Interfaces**

   The application uses interfaces for each component, including:
   - `IEventConsumer`
   - `IEventProducer`
   - `IEventEnricher`

   This ensures that each component follows the Single Responsibility Principle (SRP) and that the code is easily extensible.

## Extending the Application

### Add New Event Enrichment Strategies

You can extend the functionality by implementing a new `IEventEnricher` to perform more complex event enrichment. For example, you might want to fetch external data via an API to enrich the event.
   ```
    public class ExternalApiEventEnricher : IEventEnricher
    {
        public string EnrichEvent(string eventMessage)
        {
            // Example: Fetch additional data from an API
            var additionalData = GetAdditionalDataFromApi();
            return eventMessage + " - Enriched with API data: " + additionalData;
        }
    }
   ```
### Add More Producers/Consumers

If you want to publish or consume events to/from multiple topics or Kafka clusters, you can create new implementations of `IEventProducer` and `IEventConsumer` for each use case.

## Testing

Since the code follows Dependency Injection (DI) and SOLID principles, it is easy to write unit tests for the individual components.

You can use testing libraries like xUnit or NUnit for testing each class. For example, you can mock the `IEventConsumer` and `IEventProducer` interfaces in your tests.
   ```
    public class EventProcessorTests
    {
        [Fact]
        public void Test_ProcessEvents()
        {
            // Arrange
            var mockConsumer = new Mock<IEventConsumer>();
            var mockProducer = new Mock<IEventProducer>();
            var mockEnricher = new Mock<IEventEnricher>();

            mockConsumer.Setup(c => c.ConsumeEvent()).Returns("Test event");
            mockEnricher.Setup(e => e.EnrichEvent(It.IsAny<string>())).Returns("Test event - Enriched");

            var processor = new EventProcessor(mockConsumer.Object, mockProducer.Object, mockEnricher.Object);

            // Act
            processor.ProcessEvents();

            // Assert
            mockProducer.Verify(p => p.ProduceEvent("Test event - Enriched"), Times.Once);
        }
    }
   ```

## Conclusion

This application demonstrates a clean, modular architecture. 
It allows you to consume events from Kafka, enrich them, and produce enriched events back to Kafka with minimal effort. 
The application is easily extendable and maintainable.


