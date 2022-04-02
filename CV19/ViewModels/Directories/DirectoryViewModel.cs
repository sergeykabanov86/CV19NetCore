﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using CV19.ViewModels.Base;

namespace CV19.ViewModels.Directories
{
    internal class DirectoryViewModel : ViewModel
    {

        private readonly DirectoryInfo _DirectoryInfo;


        public IEnumerable<DirectoryViewModel> SubDirectories
        {
            get
            {

                try
                {
                    return _DirectoryInfo.EnumerateDirectories()
                        .Select(di => new DirectoryViewModel(di.FullName));
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine("UnauthorizedAccessException" + e.Message.ToString());
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }

                return Enumerable.Empty<DirectoryViewModel>();
            }
        }

        public IEnumerable<FileViewModel> Files
        {
            get
            {
                try
                {
                    return _DirectoryInfo.EnumerateFiles()
                        .Select(file => new FileViewModel(file.FullName));
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine("UnauthorizedAccessException" + e.Message.ToString());
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }

                return Enumerable.Empty<FileViewModel>();
            }
        }




        public IEnumerable<object> DirectoryItems => SubDirectories.Cast<object>().Concat(Files.Cast<object>());

        public string Name => _DirectoryInfo.Name;
        public string Path => _DirectoryInfo.FullName;
        public DateTime CreationTime => _DirectoryInfo.CreationTime;


        public DirectoryViewModel(string dirName) => _DirectoryInfo = new DirectoryInfo(dirName);


    }



    internal class FileViewModel : ViewModel
    {
        private readonly FileInfo _FileInfo;



        public string Name => _FileInfo.Name;
        public string Path => _FileInfo.FullName;
        public DateTime CreationTime => _FileInfo.CreationTime;


        public FileViewModel(string filePath) => _FileInfo = new FileInfo(filePath);

    }
}
