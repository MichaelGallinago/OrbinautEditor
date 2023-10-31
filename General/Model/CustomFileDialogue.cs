using System.Collections.Generic;
using Godot;

namespace OrbinautEditor.General.Model;

public partial class CustomFileDialog : FileDialog
{
    private static FileSelectedEventHandler _fileDialogEvent;
    
    public void Open(Dictionary<string, string> filters, FileModeEnum fileMode, 
        FileSelectedEventHandler newEvent, string title, string currentFile)
    {
        FileMode = fileMode;

        if (_fileDialogEvent is not null)
        {
            FileSelected -= _fileDialogEvent;	
        }
        _fileDialogEvent = newEvent;
        FileSelected += _fileDialogEvent;

        ClearFilters();
        foreach (KeyValuePair<string, string> filter in filters)
        {
            AddFilter(filter.Key, filter.Value);	
        }
		
        CurrentFile = currentFile;
        Title = title;
		
        Show();
    }
}