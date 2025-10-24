# 🧠 Folder Gremlin 5000

**Automate repetitive file processing tasks with ease.**  
Folder Gremlin 5000 continuously monitors a designated directory, automatically processing newly added files using your custom command templates — perfect for rendering queues, batch encoders, or any command-line workflow.

---

## 🚀 Overview

Folder Gremlin 5000 is a lightweight automation utility written in **C# (.NET)**.  
Originally designed to act as a **Blender render queue**, it can automate virtually any task that involves sequentially executing commands on incoming files.

### ✨ Core Features
- 🗂️ **Directory Watcher:** Automatically detects and processes new files.
- ⚙️ **Custom Command Templates:** Define your own CLI arguments using placeholders.
- 🧾 **Logging System:** Records all activity and errors for transparency.
- 🧠 **Self-Configuring:** Automatically generates configuration files if missing.
- 🔁 **Sequential Execution:** Ensures only one file is processed at a time.

---

## 🧩 How It Works

1. Folder Gremlin 5000 reads configuration files stored in the same directory as the executable.  
2. It continuously monitors the specified **queue folder** for new files.  
3. When a file appears, it replaces placeholders in your command template and launches the specified program.  
4. After execution, it deletes the processed file and waits for the next one.

Example use-case:  
🧱 **Render Blender files sequentially** without manually starting each render job.

---

## ⚙️ Configuration Files

When you run the program for the first time, it creates all required configuration files with default values.

| File | Purpose | Example |
|------|----------|----------|
| `file.txt` | Path to the executable that should process files | `C:/Program Files/Blender/blender.exe` |
| `command.txt` | Template command line for execution | `-b {File} -o {FileName}_{output} -F PNG -a` |
| `queue_dir.txt` | Directory to watch for new files | `C:/RenderQueue/` |
| `output.txt` | Output format or suffix | `png` |
| `log.txt` | Log of events and process output | *(auto-generated)* |

---

## 🧠 Command Template Placeholders

You can define flexible commands using these placeholders in `command.txt`:

| Placeholder | Replaced With |
|--------------|----------------|
| `{File}` | Full path to the current input file |
| `{FileName}` | File name without extension |
| `{output}` | Content of `output.txt` (e.g. file extension or folder name) |

**Example:**
-blend {File} -render {FileName}.{output}

yaml
Copy code

---

## 🪄 Example Setup (Blender Render Queue)

**1️⃣** Edit your `file.txt`  
C:/Program Files/Blender Foundation/Blender 4.0/blender.exe

markdown
Copy code

**2️⃣** Edit your `command.txt`  
-b {File} -o //output/{FileName}_{output} -F PNG -a

markdown
Copy code

**3️⃣** Edit your `queue_dir.txt`  
C:/RenderQueue/

markdown
Copy code

**4️⃣** Edit your `output.txt`  
png

yaml
Copy code

**5️⃣** Drop your `.blend` files into `C:/RenderQueue/`  
Each file will be automatically rendered one after another.

---

## ▶️ Running the Program

### 🧩 Prerequisites
- Windows 10/11 or Linux with .NET Runtime 6.0+
- Executable and configuration files in the same directory

### 🪶 Steps
1. Download or build the project:
   ```bash
   git clone https://github.com/Sewmina7/FolderGremlin5000.git
   cd FolderGremlin5000
   dotnet build
Run the program:

bash
Copy code
FolderGremlin5000.exe
The console will display:

javascript
Copy code
Starting file watcher...
Watching folder: C:/RenderQueue/
Drop files into your queue directory — Folder Gremlin 5000 will handle the rest.

## 🧾 Logging
All events are recorded in log.txt:

csharp
Copy code
[12:41:27.843] Started new session
[12:41:28.105] Found 3 files in queue
[12:41:28.260] Executing Command : -b scene.blend -F PNG
[12:55:04.100] Task finished! Deleted file scene.blend
This makes debugging and monitoring effortless.

---

## 🧱 Architecture
Language: C# (.NET 6)

Main Components:

File configuration manager

Sequential file watcher loop

Process handler using System.Diagnostics.Process

Real-time output event listener

Timestamped logging system



## 💡 Tips & Notes
If configuration files are missing, they will be created automatically.

Make sure your executable path and command arguments are valid.

You can use Folder Gremlin 5000 for other workflows — image processing, video encoding, data conversion, etc.

Edit configuration files anytime; changes apply on next iteration.



## 🚧 Future Roadmap
✅ Graceful error handling and validation

🧰 Asynchronous file watcher

🧑‍💻 GUI configuration tool

🔄 Post-processing file organization

---

### 🧑‍💻 Author
Sewmina Dilshan
Freelance software & game developer
🌐 LinkedIn • GitHub

🦾 “Set it once. Let the gremlin handle the rest.”
