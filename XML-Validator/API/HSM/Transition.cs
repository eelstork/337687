using System;

namespace Activ.Lang.HSM{
public readonly struct Transition<T>{

    public enum Op{ Enter, Exit, Cont, Chain }

    public readonly Action<T> action;
    public readonly Op operation;
    public readonly State<T> next;

    public Transition(Op operation){
        this.action = null;
        this.operation = operation;
        this.next = null;
    }

    public Transition(Action<T> action){
        this.action = action;
        this.operation = Op.Cont;
        this.next = null;
    }

    public Transition(Action<T> action, Op operation){
        this.action = action;
        this.operation = operation;
        this.next = null;
    }

    public Transition(
        Action<T> action, Op operation, State<T> next
    ){
        this.action = action;
        this.operation = operation;
        this.next = next;
    }        

}}
