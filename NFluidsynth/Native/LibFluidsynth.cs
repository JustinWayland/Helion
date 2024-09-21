﻿using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace NFluidsynth.Native
{
    internal static partial class LibFluidsynth
    {
        public const string LibraryName = "fluidsynth";
        private static bool RegisteredResolver;

        // Supports both ABI 2 and ABI 3 of Fluid Synth
        // https://abi-laboratory.pro/index.php?view=timeline&l=fluidsynth
        public static int LibraryVersion { get; private set; } = 3;

        public const int FluidOk = 0;
        public const int FluidFailed = -1;

        static LibFluidsynth()
        {
            RegisteredResolver = false;
            RegisterDllResolver();
        }

        internal static void RegisterDllResolver()
        {
            if (!RegisteredResolver)
            {
                NativeLibrary.SetDllImportResolver(typeof(LibFluidsynth).Assembly, ImportResolver);
                RegisteredResolver = true;
            }
        }

        private static string GetRuntimePath()
        {
#pragma warning disable IDE0046 // if/else collapsing produces very dense code here
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitProcess)
                return "runtimes\\win-x64\\native\\";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && Environment.Is64BitProcess)
                return "runtimes/linux-x64/native/";

            throw new NotSupportedException("This library does not support the current OS.");
#pragma warning restore IDE0046
        }

        private static string[] GetExpectedLibraryNames()
        {
#pragma warning disable IDE0046 // if/else collapsing produces very dense code here
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return ["fluidsynth.dll"];

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return ["libfluidsynth.so"];

            throw new NotSupportedException("This library does not support the current OS.");
#pragma warning restore IDE0046
        }

        private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (libraryName == LibraryName)
            {
                string runtimePath = GetRuntimePath();
                string[] libraryNames = GetExpectedLibraryNames();

                string primaryLibrary = libraryNames[0];
                if (NativeLibrary.TryLoad($"{baseDirectory}{runtimePath}{primaryLibrary}", out nint handle))
                {
                    foreach (string secondaryLibrary in libraryNames[1..])
                    {
                        _ = NativeLibrary.TryLoad($"{baseDirectory}{runtimePath}{secondaryLibrary}", out _);
                    }

                    return handle;
                }

                if (NativeLibrary.TryLoad($"{baseDirectory}{primaryLibrary}", out handle))
                {
                    foreach (string secondaryLibrary in libraryNames[1..])
                    {
                        _ = NativeLibrary.TryLoad($"{baseDirectory}{secondaryLibrary}", out _);
                    }

                    return handle;
                }

                throw new DllNotFoundException($"Could not load a suitable substitute for DllImport {libraryName}.");
            }

            return IntPtr.Zero;
        }
    }
}