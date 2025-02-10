import { Record, Union } from "../fable_modules/fable-library-js.4.24.0/Types.js";
import { list_type, int64_type, bool_type, record_type, class_type, string_type, union_type } from "../fable_modules/fable-library-js.4.24.0/Reflection.js";

export class FullTextSearch extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Exact", "Complete", "PerformanceComplete", "Fuzzy"];
    }
}

export function FullTextSearch_$reflection() {
    return union_type("Shared.Database.FullTextSearch", [], FullTextSearch, () => [[], [], [], []]);
}

export class Ontology extends Record {
    constructor(Name, Version, LastUpdated, Author) {
        super();
        this.Name = Name;
        this.Version = Version;
        this.LastUpdated = LastUpdated;
        this.Author = Author;
    }
}

export function Ontology_$reflection() {
    return record_type("Shared.Database.Ontology", [], Ontology, () => [["Name", string_type], ["Version", string_type], ["LastUpdated", class_type("System.DateTime")], ["Author", string_type]]);
}

export function Ontology_create(name, version, lastUpdated, authors) {
    return new Ontology(name, version, lastUpdated, authors);
}

export class Term extends Record {
    constructor(Accession, Name, Description, IsObsolete, FK_Ontology) {
        super();
        this.Accession = Accession;
        this.Name = Name;
        this.Description = Description;
        this.IsObsolete = IsObsolete;
        this.FK_Ontology = FK_Ontology;
    }
}

export function Term_$reflection() {
    return record_type("Shared.Database.Term", [], Term, () => [["Accession", string_type], ["Name", string_type], ["Description", string_type], ["IsObsolete", bool_type], ["FK_Ontology", string_type]]);
}

export function Term_createTerm_Z1F90B1F6(accession, name, description, isObsolete, ontologyName) {
    return new Term(accession, name, description, isObsolete, ontologyName);
}

export class TermRelationship extends Record {
    constructor(TermID, Relationship, RelatedTermID) {
        super();
        this.TermID = TermID;
        this.Relationship = Relationship;
        this.RelatedTermID = RelatedTermID;
    }
}

export function TermRelationship_$reflection() {
    return record_type("Shared.Database.TermRelationship", [], TermRelationship, () => [["TermID", int64_type], ["Relationship", string_type], ["RelatedTermID", int64_type]]);
}

export class TreeTypes_TreeTerm extends Record {
    constructor(NodeId, Term) {
        super();
        this.NodeId = NodeId;
        this.Term = Term;
    }
}

export function TreeTypes_TreeTerm_$reflection() {
    return record_type("Shared.Database.TreeTypes.TreeTerm", [], TreeTypes_TreeTerm, () => [["NodeId", int64_type], ["Term", Term_$reflection()]]);
}

export class TreeTypes_TreeRelationship extends Record {
    constructor(RelationshipId, StartNodeId, EndNodeId, Type) {
        super();
        this.RelationshipId = RelationshipId;
        this.StartNodeId = StartNodeId;
        this.EndNodeId = EndNodeId;
        this.Type = Type;
    }
}

export function TreeTypes_TreeRelationship_$reflection() {
    return record_type("Shared.Database.TreeTypes.TreeRelationship", [], TreeTypes_TreeRelationship, () => [["RelationshipId", int64_type], ["StartNodeId", int64_type], ["EndNodeId", int64_type], ["Type", string_type]]);
}

export class TreeTypes_Tree extends Record {
    constructor(Nodes, Relationships) {
        super();
        this.Nodes = Nodes;
        this.Relationships = Relationships;
    }
}

export function TreeTypes_Tree_$reflection() {
    return record_type("Shared.Database.TreeTypes.Tree", [], TreeTypes_Tree, () => [["Nodes", list_type(TreeTypes_TreeTerm_$reflection())], ["Relationships", list_type(TreeTypes_TreeRelationship_$reflection())]]);
}

