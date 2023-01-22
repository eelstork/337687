Author: TEA de Souza

# Validate XML documents

Lets a user Validate XML documents. Usage:

```
using BasicXML;
XMLValidator.DefineXML(ARG);  // ARG: the input string
```

With respect to the provided specification, assumptions are made in some cases (ref `FunctionalTests.cs`).

## Implementation notes

In this implementation, the validator is expressed as a control graph. Each state in the graph (such as `State ReadFirstInTag`) processes one character, updates model state, then returns the next control state.

Model state is represented separately, via `ValidationModel`.

Rather than writing a single validator with state-machine-like behavior, combining several validators would be more scalable.

Additionally, an alternative approach has been verified, where each control state is represented as an object:

 ```
 // Inside an InitControlGraph() function...

IControlState readFirstInTag = new TagValidationState(
    leftAngleBracket: Fail,
    rightAngleBracket: Fail,
    slash: AsClosingTag,
    doubleQuote: Fail,
    @default: AsOpeningTag
);
```

The above sample illustrates how constraining control states through certain types (such as `TagValidationState`) may help avoid oversight (handling of relevant cases predicated by the type constructor) and promotes reuse (validation actions defined as separate functions).

While the alternative is defensible, I did not find it cost effective and reverted to a more colloquial form.
