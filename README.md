# Chips & Flicks

This is a simple web application with multiple services. The start architecture is simple:

```mermaid
C4Context
    title System Context diagram for Chips & Flicks
    Enterprise_Boundary(b0, "Chips & Flicks Boundary") {
        System(hub, "Central Hub", "GraphQL API, the gateway to all data within our IT landscape")
            
        Enterprise_Boundary(b1, "Backend Services") {
            System(movies, "Movies", "Service to manage all movie information")
            System(snacks, "Snacks", "Service to suggest snacks for movies")
        }
    }
        
    Rel_Down(hub, movies, "Queries")
    Rel_Down(hub, snacks, "Queries")
```

The end result is a bit more complex:
```mermaid
C4Context
    title System Context diagram for Chips & Flicks
    Enterprise_Boundary(b0, "Chips & Flicks Boundary") {
        System(hub, "Central Hub", "GraphQL API, the gateway to all data within our IT landscape")

        Enterprise_Boundary(b1, "Backend Services") {
            System(movies, "Movies", "Service to manage all movie information")
            System(snacks, "Snacks", "Service to suggest snacks for movies")
            System(bookings, "Bookings", "Service to book tickets for movies")
            System(reviews, "Reviews", "Service to review movies and read movies reviews")
            SystemDb(cache, "Cache", "Redis cache to fake a store data")
        }
    }

    Rel_Down(hub, movies, "Queries")
    Rel_Down(hub, snacks, "Queries")
    Rel_Down(hub, reviews, "Queries")
    Rel_Down(hub, bookings, "Queries")
        
    Rel_Down(reviews, cache, "Read & Write") 
    Rel_Down(bookings, cache, "Read & Write")
```

This workshop is divided into multiple subjects, each with its own branch. The subjects are:

- [Basic Setup + REST](https://github.com/jacobduijzer/graphql-workshop)
- [GraphQL Setup + Queries](https://github.com/jacobduijzer/graphql-workshop/tree/graphql)
- [Resolvers + Data Loaders](https://github.com/jacobduijzer/graphql-workshop/tree/resolvers)
- [Mutations](https://github.com/jacobduijzer/graphql-workshop/tree/mutations)
- [Subscriptions](https://github.com/jacobduijzer/graphql-workshop/tree/subscriptions)

## Getting started

This project uses [.NET Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/). To start the project, start the ChipsFlicks.AppHost https project. This will start all the projects, including a dashboard.

![Dashboard](./docs/assets/aspire-dashboard.png)

## Basic Setup + REST



## GraphQL Setup + Queries

## Resolvers + Data Loaders

## Mutations

## Subscriptions

```graphql
mutation {
  addBooking(booking:  {
     title: "Inception",
     numberOfPeople: 3,
     eventDate: "2024-11-19"
  })
}
```

```graphql
subscription {
  bookingAdded {
     bookingNumber
     booking {
      title
     }
  }
}
```

