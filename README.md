Investigates alternative approaches to validating pseudo-XML and parsing/validation frameworks (toy project).

## HSM oriented processor

The HSM (hierarchic state machine) approach builds on FSM-like processing, adding support for entering and exiting control states. A sample implementation of the `ReadTag` control state:

```cs
 Transition<char> ReadTag(char c){
        switch (c){
            case LeftAngleBracket:
                return Fail(c);
            case RightAngleBracket:
                // Update the model and exit to the parent state.
                return new (model.EndTag, Exit);
            case Slash:
                return Fail(c);
            case DoubleQuote:
                // Update the model, then enter a child state
                return new (model.ExtendTag, Enter, ReadStringLiteral);
            default:
                // Update the model and continue `ReadTag` state at next iteration
                return new (model.ExtendTag);
        }
    }
```

This approach is deemed relatively successful. The resulting validators  (see [XMLValidator-Via-HSM](XML-Validator/XML/HSM.Validator/) are regular and easy to understand. 

Logging helps developing new parser features; the below output is extracted from functional tests:

```
  [1:2] <...
    ( Read ) BeginTag → enter
[1:3] <D...
    ( Read・ReadFirstInTag ) AsOpeningTag → chain
[1:4] <De...
    ( Read・ReadTag ) ExtendTag → cont
[1:5] <Des...
    ( Read・ReadTag ) ExtendTag → cont
```

## Multiparser

The so-called multiparser approaches combining separate parsers into one - see [the sample parser](XML-Validator/XML/Multiparser.Validator/) - I do not favor this approach; still, there are advantages in principle, such as when *adapting* existing parsers.
