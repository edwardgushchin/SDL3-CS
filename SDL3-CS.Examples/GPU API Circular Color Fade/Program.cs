using System.Diagnostics;
using System.Runtime.InteropServices;
using SDL3;

SDL.Init(SDL.InitFlags.Video);

var gpuDevice = SDL.CreateGPUDevice(SDL.GPUShaderFormat.SPIRV, false, null);
var window = SDL.CreateWindow("GPU_API_Circular_Color_Fade", 800, 600, 0);
SDL.ClaimWindowForGPUDevice(gpuDevice, window);

var tokenSource = new CancellationTokenSource();
var token = tokenSource.Token;

var taskFactory = new TaskFactory();
var task = taskFactory.StartNew(MultithreadedRendering, TaskCreationOptions.LongRunning);

while (!token.IsCancellationRequested)
{
    while (SDL.PollEvent(out var evt))
    {
        if (evt.Type == (uint)SDL.EventType.WindowCloseRequested)
        {
            tokenSource.Cancel();
        }
    }
}

void MultithreadedRendering()
{
    var stopwatch = Stopwatch.StartNew();
    while (!token.IsCancellationRequested)
    {
        var commandBuffer = SDL.AcquireGPUCommandBuffer(gpuDevice);
        SDL.AcquireGPUSwapchainTexture(
            commandBuffer, window,
            out var swapchainTexture,
            out _,
            out _
        );
    
        if (swapchainTexture != IntPtr.Zero)
        {
            var elapsed = stopwatch.Elapsed.TotalSeconds;
            var colorTargetInfo = new SDL.GPUColorTargetInfo
            {
                Texture = swapchainTexture,
                LoadOp = SDL.GPULoadOp.Clear,
                StoreOp = SDL.GPUStoreOp.Store,
                ClearColor = new SDL.FColor
                {
                    R = (float)(Math.Sin(elapsed) * 0.5 + 0.5f),
                    G = (float)(Math.Sin(elapsed + Math.PI / 2) * 0.5 + 0.5f),
                    B = (float)(Math.Sin(elapsed + Math.PI) * 0.5 + 0.5f),
                    A = 1
                }
            };
            
            var ptr = SDL.StructureToPointer<SDL.GPUColorTargetInfo>(colorTargetInfo);
            var renderPass = SDL.BeginGPURenderPass(
                commandBuffer, 
                ptr, 
                1, 
                IntPtr.Zero
            );
            Marshal.FreeHGlobal(ptr);
        
            SDL.EndGPURenderPass(renderPass);
        }

        SDL.SubmitGPUCommandBuffer(commandBuffer);
    }
}