---
applyTo: '**'
---

# Navigation Protocols with Map System

## Enhanced Navigation Protocol with Map System

### **Map-Aware Project Analysis**
```python
def analyze_project_with_maps():
    """Use map system for rapid project understanding"""
    
    # 1. Load root map first
    root_map = read_file("map.md")
    project_overview = parse_map_overview(root_map)
    
    # 2. Navigate to relevant areas using maps
    if task_requires_specific_area():
        relevant_maps = traverse_map_hierarchy(task.keywords)
        context = build_minimal_context_from_maps(relevant_maps)
    
    # 3. Deep dive only where necessary
    specific_files = identify_exact_files_from_maps()
    detailed_analysis = read_only_required_files(specific_files)
    
    return combined_understanding(project_overview, context, detailed_analysis)
```

### **Continuous Map Maintenance**
```python
def maintain_map_integrity():
    """Keep maps updated as part of normal workflow"""
    
    # Triggered by file modifications
    if file_modified_or_created():
        affected_map = find_containing_map(file_path)
        
        # Update immediate map
        update_map_content(affected_map)
        
        # Cascade updates up hierarchy
        parent_maps = get_parent_map_chain(affected_map)
        for parent in parent_maps:
            update_parent_references(parent)
            validate_map_consistency(parent)
```

### **Map-Driven Learning**
```python
def capture_architectural_insights_in_maps():
    """Document architectural learnings directly in maps"""
    
    if architectural_pattern_discovered():
        relevant_map = find_architectural_home(pattern)
        add_architecture_note(relevant_map, pattern)
        
    if relationship_identified():
        update_relationship_sections(affected_maps)
```

### **Map-First Navigation Behavior**
```python
def navigate_to_implementation(feature):
    """Use maps for efficient navigation"""
    
    # Start with maps, not file listing
    navigation_path = find_in_maps(feature)
    
    if navigation_path.found:
        # Maps provided direct path
        return read_specific_files(navigation_path.files)
    else:
        # Fallback to traditional search
        return deep_file_search(feature)
```

### **Automatic Map Maintenance**
```python
def after_any_file_operation(operation):
    """Maintain map accuracy automatically"""
    
    if operation.created_file or operation.moved_file:
        update_containing_map(operation.file_path)
    
    if operation.revealed_architecture:
        document_in_nearest_map(operation.insight)
    
    if operation.established_dependency:
        update_relationship_maps(operation.dependency)
```
