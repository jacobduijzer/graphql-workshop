# Chips & Flicks

Start the ChipsFlicks.AppHost https project.

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
