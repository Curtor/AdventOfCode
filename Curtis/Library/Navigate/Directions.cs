namespace csteeves.AdventLibrary;

using System.Collections.Generic;

public class Directions<M, N, T>
    where M : Move<N, T>
    where N : Node<N, T> {

    private int currentStep = 0;
    private List<M> directions = [];

    public IEnumerable<M> Steps => directions;
    public M CurrentStep => directions[currentStep];
    public bool Arrived => currentStep >= directions.Count;

    public Directions(List<M> moves) {
        directions.AddRange(moves);
    }

    private Directions(M goToStart) {
        directions.Add(goToStart);
    }

    public void MarkStepComplete() {
        if (!Arrived) {
            currentStep++;
        }
    }
}