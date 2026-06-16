/*
    Copyright (C) 2010  Jason Laczko

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using UnityEngine;
using System;
using System.IO;


public class dbLog {

    protected long microseconds = 1;
    protected string workingFile = "";
    private StreamWriter logfile;

	public dbLog(string filename) {
        string folderPath = Path.Combine(Application.dataPath, "Logs");

        if (!Directory.Exists(folderPath)) {
            Directory.CreateDirectory(folderPath);
        }

        workingFile = Path.Combine(folderPath, filename);

        logfile = new StreamWriter(workingFile);

        Debug.Log("🛠️ dbLog Constructor called. Target path: " + workingFile);

        logfile.AutoFlush = true;
        
        Debug.Log("Log file successfully created at: " + workingFile);
	}
	
	public dbLog() {
		//openNew(filename);
	}
	
	public virtual void close()
	{
		if (logfile != null) {
            Debug.Log("🔒 dbLog.close() was explicitly called for: " + workingFile);
            logfile.Close();    
            logfile = null;
        }	
	}
	
	public virtual string[] NextAction() {
		return null;
	}
	public virtual long PlaybackTime() {
		return 0;
	}
	
	public virtual void log(string msg, int level) {
		
	    if (logfile == null) {
            return; 
        }
        
        long tick = DateTime.Now.Ticks;
        long milliseconds = tick / TimeSpan.TicksPerMillisecond;
        microseconds = tick / 10;
        
        logfile.WriteLine( milliseconds + "\t" + msg );
	}

    // MJS function to cleanly log info with no prefixes
    public virtual void Write(string msg)
    {
        if (logfile == null) {
            return;
        }
        logfile.WriteLine(msg);
    }
}
