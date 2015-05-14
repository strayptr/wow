Set fso = CreateObject("Scripting.FileSystemObject")

Set objFile = fso.GetFile(Wscript.ScriptFullName)
strFolder = fso.GetParentFolderName(objFile) 
strPath = strFolder & "\\wow.cmd"

CreateObject("Wscript.Shell").Run strPath, 0, True

