## Pocket money

Pocket Money is a spend tracking application with the main purpose of being a sandpit for learning and trying that has a reasonable separation of data and
logic layers without any of the hassle of core code, allowing to explore specific patterns, designs or implementations

It's a work in progress so expect to see things change as it's improved, optimized and redesigned!

There's a few limitations to be aware of:
- There is alot of reflection in the project `BaseSqlDataContext` & `DataServiceInjection` being the biggest examples.
  `BaseSqlDataContext` uses reflection to construct sql queries from classes and must data queries will end up using it. The reason i didn't go with EF or similar
is to abstract the model from the persistence method so alternatives to SQL can be interchanged.
- Where data is to be persisted, there is currently two options, either having a model and a data class which is okay for internal structures that don't transfer back up
to the presentation layer.
- Alot of services are singleton given it's only ever going to be local development


## Persistence
The idea of this is to have an easy way to communicate with a storage system without having to specify the persistence method used for the structure. 
Caching can also be implemented at a core level, taking the responsibility away from the service or business layer.

### Base sql data context
`BaseSqlDataContext.cs` implements all members of `IDataContext<T>` so inheriting this into a data class should give you core
SQL functionality. The idea is that in complex data tables the base implementations can be overridden to meet the
requirements but most of the time `BaseSqlDataContext` will have you covered and creating a new model

### Data context factory
`DataContextFactory.cs` will provide you with the relevant IDataContext implementation, which can be found by either using a data class or the provided object 
has `SqlDataAttributes`.

### Data service injection
`DataServiceInjection.cs` will during startup use reflection to register all services that are an implementation of
`IDataContext<T>` to make creating a new model as easy as possible.

### Data classes
A new class implementing `IDataContext<T>` which defines most core interactions with persisting data.
You can also inherit base contexts that implement most/all the methods for the data class to work. The reason i've not made
it part of the interface is because i have an intention to not limit the persistence to sql services therefore the base
can be interchanged. This works alongside `DataServiceInjection` which will use reflection to find all of the `IDataContext`
implementations - Again this is really to save time and make creating a new model as easy as possible.

### Sql Data Attributes
Using `SqlDataAttribute` on the model for data structures that don't require any overrides for their implementation.
This will create a generic class implementing `IDataContext<T>` for you to reduce the overhead further



## Future work
Check out [the project page](https://github.com/users/luke-bunyan/projects/1/views/1). I can't make any promises on how long
this will be maintained for!





