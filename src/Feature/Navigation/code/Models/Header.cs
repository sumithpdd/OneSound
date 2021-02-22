﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSound.Feature.Navigation.Models
{
  public class Header
  {
    public string ImageUrl { get; set; }
    public string ImageAlt { get; set; }
    public string HomeLinkUrl { get; set; }
    public List<NavigationItem> Events { get; set; }
    public string ScheduleLinkUrl { get; set; }
    public bool IsExperienceEditor { get; set; }
  }
}
