public static class FSM<T> {

    public static State<T> Update(State<T> currentState, T target) {
        State<T> temp = currentState.Update(target);
        if (temp != currentState) {
            currentState.Exit(target);
            temp.Enter(target);
            return temp;
        }
        return currentState;
    }

}


