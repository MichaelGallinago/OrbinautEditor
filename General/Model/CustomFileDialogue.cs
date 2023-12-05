using System.Collections.Generic;
using Godot;

namespace OrbinautEditor.General.Model;

public partial class CustomFileDialog : FileDialog
{
    private FileSelectedEventHandler _fileDialogEvent;
    private FilesSelectedEventHandler _filesDialogEvent;
    private DirSelectedEventHandler _directoryDialogEvent;

    public void OpenDirectory(Dictionary<string, string> filters, FileModeEnum fileMode, 
        DirSelectedEventHandler newEvent, string title, string currentName)
    {
        FileMode = fileMode;
        CurrentDir = currentName;

        if (_directoryDialogEvent is not null)
        {
            DirSelected -= _directoryDialogEvent;	
        }
        _directoryDialogEvent = newEvent;
        DirSelected += _directoryDialogEvent;

        OpenDialogWindow(filters, title);
    }
    
    public void OpenFile(Dictionary<string, string> filters, FileModeEnum fileMode, 
        FileSelectedEventHandler newEvent, string title, string currentName)
    {
        FileMode = fileMode;
        CurrentFile = currentName;

        if (_fileDialogEvent is not null)
        {
            FileSelected -= _fileDialogEvent;	
        }
        _fileDialogEvent = newEvent;
        FileSelected += _fileDialogEvent;

        OpenDialogWindow(filters, title);
    }
    
    public void OpenFiles(Dictionary<string, string> filters, FileModeEnum fileMode, 
        FilesSelectedEventHandler newEvent, string title, string currentName)
    {
        FileMode = fileMode;
        CurrentFile = currentName;

        if (_filesDialogEvent is not null)
        {
            FilesSelected -= _filesDialogEvent;	
        }
        _filesDialogEvent = newEvent;
        FilesSelected += _filesDialogEvent;

        OpenDialogWindow(filters, title);
    }

    private void OpenDialogWindow(Dictionary<string, string> filters, string title)
    {
        ClearFilters();
        foreach (var filter in filters)
        {
            AddFilter(filter.Key, filter.Value);	
        }

        Title = title;
		
        Show();
    }
}