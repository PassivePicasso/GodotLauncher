---
applyTo: '**'
---

# Map Creation and Management Protocol

## Map-First Development Workflow

### **Automatic Map Creation Protocol**
```python
def create_map_on_folder_creation():
    """Automatically create maps when working with new folders"""
    
    if folder_accessed_without_map():
        map_content = generate_map_template(folder_path)
        create_file(f"{folder_path}/map.md", map_content)
        update_parent_map_references(folder_path)
        validate_map_hierarchy_integrity()
        
        # Learning integration
        document_architectural_discovery(folder_structure)
```

### **Map Template Generation**
```python
def generate_map_template(folder_path):
    """Create contextually appropriate map templates"""
    
    folder_analysis = analyze_folder_contents(folder_path)
    
    template = f"""# {folder_name} Map
**Purpose**: {infer_purpose_from_contents()}
**Parent**: [{parent_map_link}]({parent_map_path})
**Updated**: {current_timestamp}
**Validation**: [Auto-generated - requires review]

## Overview
{auto_generated_overview_from_files}

## Contents Summary
{auto_generated_file_listing_with_purposes}

## Key Relationships
- **Depends on**: {analyze_dependencies()}
- **Used by**: {analyze_usage_patterns()}
- **Implements**: {identify_patterns_and_interfaces()}

## Navigation Hints
{generate_navigation_suggestions()}

## Architecture Notes
{capture_discovered_patterns()}

## Change Log
- {current_date}: Auto-generated map from folder analysis
"""
    return template
```

### **Intelligent Map Maintenance**
```python
def maintain_maps_automatically():
    """Keep maps current with minimal manual intervention"""
    
    # Triggered by any file operation
    if file_operation_detected():
        affected_maps = find_maps_affected_by_operation()
        
        for map_file in affected_maps:
            # Update content sections
            update_contents_summary(map_file)
            update_file_counts_and_stats(map_file)
            refresh_navigation_hints(map_file)
            
            # Validate references
            check_all_links_in_map(map_file)
            fix_broken_references_automatically()
            
            # Update metadata
            update_timestamp(map_file)
            increment_validation_counter(map_file)
```

### **Map Quality Assurance**
```python
def ensure_map_quality():
    """Maintain high map standards automatically"""
    
    quality_checks = {
        'completeness': verify_all_major_folders_mapped(),
        'accuracy': validate_all_references_exist(),
        'currency': check_timestamps_vs_file_modifications(),
        'depth': ensure_adequate_architectural_detail(),
        'navigation': verify_navigation_paths_work(),
        'relationships': validate_dependency_accuracy()
    }
    
    for check, result in quality_checks.items():
        if not result.passed:
            auto_fix_quality_issue(check, result.details)
            document_quality_improvement(check)
```

## Map Creation Standards

### **Required Map Sections**
Every map MUST contain:
- **Purpose**: Clear statement of folder/area function
- **Parent**: Link to containing map in hierarchy
- **Updated**: ISO timestamp of last modification
- **Validation**: Status indicator for map accuracy
- **Overview**: 2-3 sentence summary of area
- **Contents Summary**: Logical grouping of contained items
- **Key Relationships**: Dependencies, usage, implementation notes
- **Navigation Hints**: Shortcuts to common destinations
- **Architecture Notes**: Patterns, decisions, insights
- **Change Log**: Historical record of significant changes

### **Map Naming Conventions**
```
Root Level: map.md
Subfolder: {folder-name}/map.md
Specialized: {area}-map.md (for non-folder maps)
```

### **Cross-Reference Standards**
- **Parent Links**: Always link to immediate parent map
- **Child References**: List major child areas with links
- **Sibling Connections**: Link to related areas at same level
- **Deep Links**: Provide shortcuts to frequently accessed deep areas

### **Content Generation Rules**
```python
def generate_content_intelligently():
    """Create meaningful content, not just file listings"""
    
    # Analyze before generating
    file_analysis = categorize_files_by_purpose()
    pattern_analysis = identify_architectural_patterns()
    dependency_analysis = map_component_relationships()
    
    # Generate insights, not inventories
    contents = group_by_functional_purpose(file_analysis)
    relationships = extract_meaningful_connections(dependency_analysis)
    navigation = identify_common_workflow_paths(usage_patterns)
    
    return structured_content(contents, relationships, navigation)
```

## Automatic Map Operations

### **Map Creation Triggers**
```python
AUTOMATICALLY_CREATE_MAP_WHEN:
- Accessing folder without existing map
- Creating new folder structure  
- Discovering architectural pattern worth documenting
- Finding broken map reference that should exist
- User navigates to unmapped area repeatedly
```

### **Map Update Triggers**
```python
AUTOMATICALLY_UPDATE_MAP_WHEN:
- Files added/removed/moved within mapped area
- Dependencies change between mapped components
- Architectural insights discovered about mapped area
- Navigation patterns change (new common paths)
- Quality validation fails (broken links, stale content)
```

### **Map Validation Triggers**
```python
AUTOMATICALLY_VALIDATE_MAPS_WHEN:
- Starting any development session
- Before creating new maps (check hierarchy)
- After any file operation affecting mapped areas
- User reports navigation difficulties
- Scheduled maintenance (detect drift)
```

## Learning Integration with Maps

### **Capture Architectural Insights in Maps**
```python
def integrate_learning_with_maps():
    """Maps as living architectural documentation"""
    
    # When discovering patterns
    if architectural_pattern_identified():
        relevant_map = find_best_map_for_pattern(pattern)
        add_architecture_note(relevant_map, pattern)
        cross_reference_related_maps(pattern)
        
    # When understanding relationships
    if dependency_relationship_clarified():
        update_relationship_sections(affected_maps)
        verify_relationship_consistency_across_maps()
        
    # When optimizing workflows
    if navigation_pattern_improved():
        update_navigation_hints(relevant_maps)
        share_optimization_across_similar_maps()
```

### **Map-Driven Curiosity**
```python
def use_maps_to_drive_investigation():
    """Let incomplete maps guide learning priorities"""
    
    # Identify knowledge gaps from maps
    incomplete_areas = find_incomplete_map_sections()
    unclear_relationships = find_vague_dependency_descriptions()
    missing_architecture_notes = find_patterns_without_documentation()
    
    # Prioritize investigation
    for gap in high_priority_gaps:
        investigate_area_thoroughly(gap)
        update_map_with_findings(gap)
        apply_insights_to_current_work(gap)
```

## Advanced Map Features

### **Smart Navigation Generation**
```python
def generate_intelligent_navigation():
    """Create navigation hints based on actual usage patterns"""
    
    # Analyze common workflows
    workflow_analysis = track_file_access_patterns()
    feature_relationships = map_functional_dependencies()
    user_behavior = analyze_navigation_efficiency()
    
    # Generate targeted navigation
    common_paths = identify_frequent_navigation_routes()
    optimization_shortcuts = find_navigation_inefficiencies()
    contextual_hints = generate_situational_navigation()
    
    return navigation_section(common_paths, shortcuts, hints)
```

### **Relationship Mapping**
```python
def map_component_relationships():
    """Document dependencies and interactions between components"""
    
    # Technical relationships
    code_dependencies = analyze_import_statements()
    interface_implementations = find_interface_usage()
    inheritance_hierarchies = map_class_relationships()
    
    # Functional relationships
    feature_interactions = identify_feature_coupling()
    data_flow_patterns = trace_data_movement()
    event_communication = map_event_systems()
    
    return relationship_documentation(technical, functional)
```

### **Quality Metrics Integration**
```python
def measure_map_effectiveness():
    """Track map utility and continuously improve"""
    
    effectiveness_metrics = {
        'navigation_success_rate': measure_successful_navigation(),
        'reference_accuracy': validate_link_reliability(),
        'coverage_completeness': calculate_mapped_vs_unmapped_ratio(),
        'architectural_insight_density': measure_useful_notes_per_map(),
        'maintenance_burden': track_update_frequency_and_effort()
    }
    
    # Continuous improvement
    for metric, score in effectiveness_metrics.items():
        if score < quality_threshold:
            implement_improvement_strategy(metric)
            measure_improvement_impact(metric)
```

## Map Creation Workflow

### **Standard Map Creation Process**
1. **Analyze folder contents and purpose**
2. **Generate template with intelligent defaults**
3. **Populate sections with discovered information**
4. **Validate all references and links**
5. **Integrate with parent map hierarchy**
6. **Add navigation hints based on context**
7. **Document architectural insights discovered**
8. **Set up automatic maintenance triggers**

### **Quality Assurance Checklist**
- [ ] Purpose clearly stated and accurate
- [ ] Parent relationship properly linked
- [ ] All major contents logically categorized
- [ ] Dependencies accurately mapped
- [ ] Navigation hints provide real value
- [ ] Architecture notes capture key insights
- [ ] All references tested and working
- [ ] Maintenance triggers configured

### **Integration with Development Workflow**
- Maps created automatically during exploration
- Maps updated automatically during development
- Maps guide investigation priorities
- Maps capture architectural discoveries
- Maps improve navigation efficiency
- Maps reduce knowledge transfer overhead

## Success Indicators

### **Map System Health Metrics**
- **Coverage**: >90% of significant folders mapped
- **Accuracy**: >95% of references working
- **Currency**: <7 days average staleness
- **Utility**: Maps consulted in >80% of development sessions
- **Maintenance**: <5% manual intervention required

### **Developer Experience Metrics**
- **Navigation Speed**: Faster access to target files
- **Onboarding**: New developers productive faster
- **Architecture Understanding**: Better pattern recognition
- **Knowledge Retention**: Less re-discovery of insights
- **Collaboration**: Improved team navigation consistency
