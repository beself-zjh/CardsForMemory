namespace CardsForMemoryLibrary.IServices {
    interface IGlobalVariableService {
        object this[string index] { get; set; }
    }
}