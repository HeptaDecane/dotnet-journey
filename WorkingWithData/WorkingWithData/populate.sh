use bookStore

db.createCollection('Books')
db.createCollection('Customers')
db.createCollection('MembershipTypes')

db.Books.insertMany([
    {
        _id : 1,
        Name : "Design Patterns",
        Price : 54.93,
        Category : "Computers",
        Author : "Ralph Johnson"
    },
    {
        _id : 2,
        Name : "Clean Code",
        Price : 43.15,
        Category : "Computers",
        Author : "Robert C. Martin"
    },
    {
        _id : 3,
        Name : "Introduction to Algorithms",
        Price : 32.98,
        Category : "Computers",
        Author : "Ronald Rivest"
    }

])

db.MembershipTypes.insertMany([
    {
        _id: 1,
        Tier: "Silver",
        SignUpFee: 5.99,
        DurationInMonths: 3,
        DiscountPercentage: 10 
    },
    {
        _id: 2,
        Tier: "Gold",
        SignUpFee: 7.99,
        DurationInMonths: 6,
        DiscountPercentage: 15
    },
    {
        _id: 3,
        Tier: "Diamond",
        SignUpFee: 10.99,
        DurationInMonths: 12,
        DiscountPercentage: 25
    }
])

db.MembershipTypes.find()

db.Customers.insertMany([
    {
        _id: 1,
        Username: "mustardfly",
        MembershipType: 1
    },
    {
        _id: 2,
        Username: "drumolive",
        MembershipType: 3
    },
    {
        _id: 3,
        Username: "squidpool",
        MembershipType: 2
    },
    {
        _id: 4,
        Username: "foxakira",
        MembershipType: 2
    }
])
