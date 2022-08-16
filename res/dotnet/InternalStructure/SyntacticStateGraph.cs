using System.Linq;
using System.Collections.Generic;

namespace Orkestra.InternalStructure;

public class SyntacticStateGraph
{
    public StackLinkedList TokenList { get; private set; }
    public List<Rule> RuleList { get; private set; }
    public SubRuleDictionary Dictionary { get; private set; }

    public SyntacticStateGraph(IEnumerable<INode> tokens, IEnumerable<Rule> rules)
    {
        this.TokenList = new StackLinkedList(tokens);
        this.RuleList = new List<Rule>(rules);
        this.Dictionary = new SubRuleDictionary(rules);
    }

    public INode DepthFirstSearch()
    {
        depthFirstSearch();
        var header = TokenList.FirstOrDefault();
        var main = header.Next;
        return main.Value;
    }

    private StackLinkedListNode searchMatches(StackLinkedListNode initial, SubRule[] rules)
    {
        if (rules.Length == 0)
            return null;

        foreach (var rule in rules)
        {
            var newNode = testMatch(initial, rule);

            if (newNode == null)
                continue;
            return newNode;
        }

        return null;
    }

    private StackLinkedListNode testMatch(StackLinkedListNode initial, SubRule rule)
    {
        var it = initial;
        var tokensIterator = rule.RuleTokens.GetEnumerator();
        var nodes = new List<INode>();

        while (tokensIterator.MoveNext())
        {   
            if (it == null || it.Value == null)
                return null;
            
            if (!it.Value.Is(tokensIterator.Current))
                return null;
            nodes.Add(it.Value);
            
            it = it.Next;
        }

        RuleMatch match = new RuleMatch(rule, nodes.ToArray());
        StackLinkedListNode newNode = new StackLinkedListNode();
        newNode.Value = match;

        initial.Previous.Connect(newNode);
        newNode.Connect(it);

        return newNode;
    }
    
    private void reduce()
    {

    }

    private void reverse()
    {

    }

    private void depthFirstSearch()
    {
        var stack = new Stack<StackLinkedListNode>();

        var header = TokenList.FirstOrDefault();
        var first = header.Next;
        stack.Push(first);

        while (stack.Count > 0)
        {
            
        }
    }
}