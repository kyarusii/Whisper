# Whisper

## Usage
See [InputProcessor.cs] and [InputHandler.cs].  
Both classes don't have any reference each other but delegates are bindable and executable.  
They works like MessageSystem of Unity.  

## Installation
### NPM Registry
[![NPM](https://nodei.co/npm/com.calci.whisper.png?compact=true)](https://npmjs.org/package/com.calci.whisper)

Replace stable version at version definition in json `x.x.x`  
example) `"com.calci.whisper": "1.0.0"`  
```json
{
    "dependencies": {
        "com.calci.assetlens": "x.x.x"
    }
}
```

```json
{
    "scopedRegistries": [
        {
            "name": "npm",
            "url": "https://registry.npmjs.org",
            "scopes": [
                "com.calci"
            ]
        }
    ]
}
```

[InputProcessor.cs]: Assets/Calci.Whisper.Example/Assembly1/InputProcessor.cs
[InputHandler.cs]: Assets/Calci.Whisper.Example/Assembly2/InputHandler.cs
