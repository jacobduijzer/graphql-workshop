# Chips & Flicks

This is a simple web application with multiple services. The architecture is simple:

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

This workshop is divided into multiple subjects, each with its own branch. The subjects are:

- [Basic Setup + REST](https://github.com/jacobduijzer/graphql-workshop)
- [GraphQL Setup + Queries](https://github.com/jacobduijzer/graphql-workshop/tree/graphql)
- [Resolvers + Data Loaders](https://github.com/jacobduijzer/graphql-workshop/tree/resolvers)
- [Mutations](https://github.com/jacobduijzer/graphql-workshop/tree/mutations)
- [Subscriptions](https://github.com/jacobduijzer/graphql-workshop/tree/subscriptions)
